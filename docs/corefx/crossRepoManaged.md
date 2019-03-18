# Managed part(mscorelib)

## CLR repo

First compile repo

### Win
```
git clean -fdx
build -skiptests -release
```
### Linux
```
git clean -fdx
./build.sh -skiptests -release
```

After every change recompile only managed part
### Win
```
build -release -skiptests -skipnative -skipbuildpackages
```
### Linux
```
./build.sh -release -skiptests -skipnative -skipbuildpackages
```

## CoreFx repo

First compile repo and link local build
### Win
```
git clean -fdx
build.cmd -c Release /p:CoreCLROverridePath=C:\Projects\coreclr\bin\Product\Windows_NT.x64.Release
```
### Linux
```
clean -all
./build.sh -c Release /p:CoreCLROverridePath=C:\Projects\coreclr\bin\Product\Windows_NT.x64.Release
```
You don't need to rebuild if CLR bits change but you can "relink" if you need
### Win
```
build.cmd -restore -c Release /p:CoreCLROverridePath=C:\Projects\coreclr\bin\Product\Windows_NT.x64.Release
```
### Linux
```
../Tool/msbuild.sh -restore -c Release /p:CoreCLROverridePath=C:\Projects\coreclr\bin\Product\Windows_NT.x64.Release
```

Official guide https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/developer-guide.md#testing-with-private-coreclr-bits  
Maryam Ariyan way https://github.com/dotnet/coreclr/pull/16151#issuecomment-362356957  
CoreClr runtime Updated(on Corefx) https://github.com/dotnet/corefx/commits?author=dotnet-maestro-bot  
Mirror changes from dotnet/coreclr https://github.com/dotnet/corefx/pulls?q=is%3Apr+author%3Adotnet-bot  
If build with local clr coreclr fail one solution could be https://github.com/dotnet/coreclr/pull/18524#issuecomment-398396986  
