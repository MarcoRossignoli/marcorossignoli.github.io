# Managed part(mscorelib)

## CLR repo

First compile repo

### Win
```
clean -all
build -skiptests -release
```
### Linux
```
./clean.sh -all
./build.sh -skiptests -release
```


After every change recompile only managed part
### Win
```
build -skiptests -skipnative -release
```
### Linux
```
./build.sh -skiptests -skipnative -release
```
## Update CLR repo after coding
### Win
```
build -skiptests -skipnative -release
```
### Linux
```
./build.sh -skiptests -skipnative -release
```


## CoreFx repo

First compile repo and link local build
### Win
```
clean -all
build.cmd -- /p:CoreCLROverridePath=d:\git\coreclr\bin\Product\Windows_NT.x64.Release\
```
### Linux
```
clean -all
./build.sh -- /p:CoreCLROverridePath=.../coreclr/bin/Product/Linux.x64.Release/
```
You don't need to rebuild if CLR bits change but you can "relink" if you need
### Win
```
msbuild /p:CoreCLROverridePath=d:\git\coreclr\bin\Product\Windows_NT.x64.Release\ ./external/runtime/runtime.depproj
```
### Linux
```
../Tool/msbuild.sh /p:CoreCLROverridePath=.../coreclr/bin/Product/Linux.x64.Release/ ./external/runtime/runtime.depproj
```

## Tests with msbuild
### Win
```
msbuild /v:m /t:RebuildAndTest "/p:XunitOptions=-trait MyTrait=MyTrait"  System.Runtime.Extensions.Tests.csproj
msbuild /v:m /t:RebuildAndTest "/p:XunitOptions=-method System.IO.Tests.PathTests.Try_GetTempPath_Default"  System.Runtime.Extensions.Tests.csproj
```
### Linux
```
../../Tools/msbuild.sh /v:m /t:RebuildAndTest "/p:XunitOptions=-trait MyTrait=MyTrait"  System.Runtime.Extensions.Tests.csproj
../../Tools/msbuild.sh /v:m /t:RebuildAndTest "/p:XunitOptions=-method System.IO.Tests.PathTests.Try_GetTempPath_Default"  System.Runtime.Extensions.Tests.csproj
```
Maryam Ariyan way https://github.com/dotnet/coreclr/pull/16151#issuecomment-362356957  
[MsBuild Command line for switches](https://msdn.microsoft.com/en-us/library/ms164311.aspx)  
[XUnit method with msbuild](https://github.com/dotnet/buildtools/blob/master/Documentation/test-targets-usage.md#run-a-single-xunit-method)  
CoreClr runtime Updated(on Corefx) https://github.com/dotnet/corefx/commits?author=dotnet-maestro-bot  
Mirror changes from dotnet/coreclr https://github.com/dotnet/corefx/pulls?q=is%3Apr+author%3Adotnet-bot  
