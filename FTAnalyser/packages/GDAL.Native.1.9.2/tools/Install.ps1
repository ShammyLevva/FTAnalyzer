param($installPath, $toolsPath, $package, $project)

Import-Module (Join-Path $toolsPath Utility.psd1)
$nativeBinDirectory = Join-Path $installPath "gdal"
if ($project.Type -eq 'Web Site') {
    $projectRoot = Get-ProjectRoot $project
    if (!$projectRoot) {
        return;
    }

    $binDirectory = Join-Path $projectRoot "bin\gdal"
    $libDirectory = Join-Path $installPath "lib\net40"
    if (test-path $libDirectory -pathType container) {
        Add-FilesToVSFolder $libDirectory $binDirectory "*.dll"
    }
    Add-FilesToVSFolder $nativeBinDirectory $binDirectory "*"
}
elseif($project.ExtenderNames -contains "WebApplication") {
    # _bin_deployableAssemblies is recognized by MVC3 MSBuild commands
    $depAsm = Get-EnsuredFolder $project "_bin_deployableAssemblies";
	if($depAsm)
    {
        #Write-Host "Install.ps1:" $nativeBinDirectory
        Add-ItemsToProject $depAsm $nativeBinDirectory
    }
}
else {
    $buildStep = Get-XCopyBuildStep $installPath  "gdal"
    Add-BuildStep $project "Post" $buildStep
}
Remove-Module Utility