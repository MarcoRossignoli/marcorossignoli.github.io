param (
[string]$driveLetter = "C:\"
)

$path = Join-Path -Path $driveLetter -ChildPath "git\coreclr\build.cmd"

if (Test-Path -Path $path)
{
    & $path -skiptests -skipnative -debug        
}
else
{
    Write-Host Path $path not found
}

Write-Host ---------------
Write-Host CoreClr rebuilt
Write-Host ---------------

$path = Join-Path -Path  $driveLetter -ChildPath "git\corefx\build.cmd"


if (Test-Path -Path $path)
{
    $clrPath = (Join-Path -Path $driveLetter -ChildPath git\coreclr\bin\Product\Windows_NT.x64.Debug\)
    & $path  /p:CoreCLROverridePath=$clrPath ./external/runtime/runtime.depproj
}
else
{
    Write-Host Path $path not found
}

Write-Host ---------------
Write-Host CoreFx rebuilt
Write-Host ---------------