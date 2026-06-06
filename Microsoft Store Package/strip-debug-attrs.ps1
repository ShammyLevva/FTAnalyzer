param(
    [string]$LayoutDir,
    [string]$MonoCecilDll
)

if ([string]::IsNullOrEmpty($MonoCecilDll) -or -not (Test-Path $MonoCecilDll)) {
    Write-Error "Mono.Cecil not found at: '$MonoCecilDll'"
    exit 1
}

if (-not (Test-Path $LayoutDir)) {
    Write-Host "  Layout dir not found, skipping: $LayoutDir"
    exit 0
}

Add-Type -Path $MonoCecilDll

$targets = 'Common.Logging.dll', 'Common.Logging.Core.dll'

foreach ($name in $targets) {
    $path = Join-Path $LayoutDir $name
    if (-not (Test-Path $path)) {
        Write-Host "  skip $name (not in layout)"
        continue
    }

    $asm = [Mono.Cecil.AssemblyDefinition]::ReadAssembly($path)

    $toRemove = @($asm.CustomAttributes | Where-Object {
        $_.AttributeType.FullName -eq 'System.Diagnostics.DebuggableAttribute'
    })

    if ($toRemove.Count -eq 0) {
        $asm.Dispose()
        Write-Host "  skip $name (no DebuggableAttribute found)"
        continue
    }

    foreach ($attr in $toRemove) {
        [void]$asm.CustomAttributes.Remove($attr)
    }

    # Remove strong-name signature so the modified assembly can be written.
    # .NET 5+ does not enforce strong-name verification at runtime.
    $asm.Name.HasPublicKey = $false
    $asm.Name.PublicKey = [byte[]]@()
    $asm.Name.PublicKeyToken = [byte[]]@()
    foreach ($mod in $asm.Modules) {
        $mod.Attributes = $mod.Attributes -band (-bnot 8)  # 8 = ModuleAttributes.StrongNameSigned
    }

    $tempPath = "$path.tmp"
    $wp = [Mono.Cecil.WriterParameters]::new()
    $asm.Write($tempPath, $wp)
    $asm.Dispose()
    Move-Item -Force $tempPath $path

    Write-Host "  stripped DebuggableAttribute from $name"
}
