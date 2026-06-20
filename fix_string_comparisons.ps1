param([switch]$DryRun)

$buildOutput = Get-Content "$PSScriptRoot\build_analysis.txt"

# ---- Parse warnings ----------------------------------------------------------------

# Deduplicate to unique (File, Line, Method) — col is the start of the invocation
# expression, not the method name, so it can't reliably locate the right call in a chain.
# Instead we'll fix ALL occurrences of MethodName on each affected line.
$ca1307 = $buildOutput | ForEach-Object {
    if ($_ -match "^(.+\.cs)\((\d+),(\d+)\).*warning CA1307.*'string\.(\w+)\(") {
        [PSCustomObject]@{ File = $matches[1]; Line = [int]$matches[2]; Method = $matches[4] }
    }
} | Where-Object { $_ } |
  Select-Object File, Line, Method |
  Sort-Object File, Line, Method -Unique

$ca1309files = ($buildOutput | ForEach-Object {
    if ($_ -match "^(.+\.cs)\((\d+),(\d+)\).*warning CA1309") { $matches[1] }
} | Where-Object { $_ } | Sort-Object -Unique)

Write-Host "CA1307 unique (file,line,method): $($ca1307.Count)"
Write-Host "CA1309 files:                     $($ca1309files.Count)"

# ---- Helpers -----------------------------------------------------------------------

# Walks forward from startPos (must be the opening '(') and returns the index of the
# matching ')'. Returns -1 if the call spans multiple lines.
function Find-ClosingParen([string]$text, [int]$startPos) {
    $depth    = 0
    $inStr    = $false
    $verbatim = $false
    $i        = $startPos

    while ($i -lt $text.Length) {
        $c = $text[$i]

        if ($inStr) {
            if ($verbatim) {
                if ($c -eq '"' -and ($i + 1 -lt $text.Length) -and $text[$i+1] -eq '"') {
                    $i += 2; continue
                }
                if ($c -eq '"') { $inStr = $false }
            } else {
                if ($c -eq '\' -and ($i + 1 -lt $text.Length)) { $i += 2; continue }
                if ($c -eq '"') { $inStr = $false }
            }
        } else {
            if ($c -eq '@' -and ($i + 1 -lt $text.Length) -and $text[$i+1] -eq '"') {
                $inStr = $true; $verbatim = $true; $i += 2; continue
            }
            if ($c -eq '"')  { $inStr = $true; $verbatim = $false }
            elseif ($c -eq "'") {
                $i++
                while ($i -lt $text.Length -and $text[$i] -ne "'") {
                    if ($text[$i] -eq '\') { $i++ }
                    $i++
                }
            }
            elseif ($c -eq '(') { $depth++ }
            elseif ($c -eq ')') {
                $depth--
                if ($depth -eq 0) { return $i }
            }
        }
        $i++
    }
    return -1
}

function Select-Comparison([string]$method, [string]$argText) {
    switch ($method) {
        "Replace" { return "StringComparison.Ordinal" }
        "IndexOf" {
            if ($argText.Trim().StartsWith("'")) { return "StringComparison.Ordinal" }
            return "StringComparison.OrdinalIgnoreCase"
        }
        default   { return "StringComparison.OrdinalIgnoreCase" }
    }
}

# Fixes every occurrence of .MethodName(...) on a line that doesn't already have
# StringComparison, working right-to-left so earlier insertions don't shift positions.
function Fix-MethodOnLine([string]$line, [string]$method, [string]$comparison) {
    $needle = ".$method("
    $result = $line
    $searchFrom = $result.Length

    while ($searchFrom -gt $needle.Length) {
        $pos = $result.LastIndexOf($needle, $searchFrom - 1)
        if ($pos -lt 0) { break }

        $parenPos = $pos + $needle.Length - 1   # index of '('
        $closePos = Find-ClosingParen $result $parenPos
        if ($closePos -lt 0) { $searchFrom = $pos; continue }

        $argText = $result.Substring($parenPos + 1, $closePos - $parenPos - 1)

        if ($argText -notmatch 'StringComparison') {
            # Replace(char, char) has no StringComparison overload — skip
            if ($method -eq "Replace" -and $argText.TrimStart().StartsWith("'")) {
                $searchFrom = $pos; continue
            }
            # Choose Ordinal vs OrdinalIgnoreCase based on method + arg
            $cmp = if ($method -eq "IndexOf") { Select-Comparison $method $argText } else { $comparison }
            $sep = if ($argText.Trim() -eq "") { "" } else { ", " }
            $result = $result.Substring(0, $closePos) + "$sep$cmp" + $result.Substring($closePos)
        }

        $searchFrom = $pos
    }
    return $result
}

# ---- CA1309 fixes (simple text replacement) ----------------------------------------

$ca1309fixed = 0
foreach ($file in $ca1309files) {
    $original = [System.IO.File]::ReadAllText($file)
    $updated  = $original `
        -replace 'StringComparison\.CurrentCultureIgnoreCase',   'StringComparison.OrdinalIgnoreCase' `
        -replace 'StringComparison\.InvariantCultureIgnoreCase', 'StringComparison.OrdinalIgnoreCase' `
        -replace 'StringComparison\.CurrentCulture\b',           'StringComparison.Ordinal' `
        -replace 'StringComparison\.InvariantCulture\b',         'StringComparison.Ordinal'

    if ($updated -ne $original) {
        if (-not $DryRun) {
            [System.IO.File]::WriteAllText($file, $updated, [System.Text.UTF8Encoding]::new($false))
        }
        Write-Host "CA1309 fixed: $($file -replace '.*\\FTAnalyzer[^\\]*\\','')"
        $ca1309fixed++
    }
}

# ---- CA1307 fixes (method-name search, all occurrences per line) -------------------

$ca1307fixed   = 0
$ca1307skipped = 0

$byFile = $ca1307 | Group-Object File

foreach ($group in $byFile) {
    $filePath = $group.Name
    $raw      = [System.IO.File]::ReadAllText($filePath)
    $crlf     = $raw.Contains("`r`n")
    $lines    = $raw -replace "`r`n","`n" -replace "`r","`n" -split "`n"
    $dirty    = $false

    # Group warnings by line; process bottom-to-top
    $byLine = $group.Group | Group-Object Line | Sort-Object { [int]$_.Name } -Descending

    foreach ($lineGroup in $byLine) {
        $li = [int]$lineGroup.Name - 1
        if ($li -ge $lines.Length) { $ca1307skipped += $lineGroup.Group.Count; continue }

        $row     = $lines[$li]
        $methods = $lineGroup.Group | Select-Object -ExpandProperty Method | Sort-Object -Unique

        foreach ($method in $methods) {
            $comparison = Select-Comparison $method ""
            $before = $row
            $row    = Fix-MethodOnLine $row $method $comparison
            if ($row -ne $before) { $ca1307fixed++ }
        }

        $lines[$li] = $row
        $dirty = $true
    }

    if ($dirty -and -not $DryRun) {
        $sep     = if ($crlf) { "`r`n" } else { "`n" }
        $newText = $lines -join $sep
        [System.IO.File]::WriteAllText($filePath, $newText, [System.Text.UTF8Encoding]::new($false))
    }
}

Write-Host ""
Write-Host "CA1309 files updated : $ca1309fixed"
Write-Host "CA1307 lines fixed   : $ca1307fixed"
Write-Host "CA1307 lines skipped : $ca1307skipped"
if ($DryRun) { Write-Host "(DRY RUN — no files written)" }
