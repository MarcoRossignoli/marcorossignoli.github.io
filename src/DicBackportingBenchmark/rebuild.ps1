param (
[string]$driveLetter = "C:\"
)

function Create-Directory([string[]] $path) {
  if (!(Test-Path $path)) {
    New-Item -path $path -force -itemType "Directory" | Out-Null
  }
}

function DownloadVsWhere {
  set-strictmode -version 2.0
  $ErrorActionPreference = "Stop"
  [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12  
  $toolsRoot = Join-Path $PSScriptRoot "tools"
  $vsWhereDir = Join-Path $toolsRoot "vswhere"
  $vsWhereExe = Join-Path $vsWhereDir "vswhere.exe"

  if (!(Test-Path $vsWhereExe)) {
    Create-Directory $vsWhereDir
    Write-Host "Downloading vswhere"
    Invoke-WebRequest "https://github.com/Microsoft/vswhere/releases/download/2.5.2/vswhere.exe" -OutFile $vswhereExe
  }  
  return $vswhereExe
}

function LocateMsBuild {
  $toolsRoot = Join-Path $PSScriptRoot "tools"
  $vsWhereDir = Join-Path $toolsRoot "vswhere"
  $vsWhereExe = Join-Path $vsWhereDir "vswhere.exe"

  $path = & $vswhereExe -latest -products * -requires Microsoft.Component.MSBuild -property installationPath
  $path = join-path $path 'MSBuild\15.0\Bin\MSBuild.exe'
  return $path
}

$msbuild = LocateMsBuild

$path = Join-Path -Path $driveLetter -ChildPath "git\coreclr\build.cmd"

if (Test-Path -Path $path)
{
    & $path -skiptests -skipnative
}
else
{
    Write-Host Path $path not found
}

Write-Host ---------------
Write-Host CoreClr rebuilt
Write-Host ---------------

$path = Join-Path -Path $driveLetter -ChildPath "git\corefx"
$clrPath = (Join-Path -Path $driveLetter -ChildPath git\coreclr\bin\Product\Windows_NT.x64.Debug\)
if (Test-Path -Path $clrPath)
{
    Push-Location $path
    Write-Host $msbuild /p:CoreCLROverridePath=$clrPath ./external/runtime/runtime.depproj
    & $msbuild /p:CoreCLROverridePath=$clrPath ./external/runtime/runtime.depproj
    Pop-Location
}
else
{
    Write-Host Path $clrPath not found
}

Write-Host ---------------
Write-Host CoreFx rebuilt
Write-Host ---------------