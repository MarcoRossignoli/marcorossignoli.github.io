# marcorossignoli.github.io
* dotnet core repos https://github.com/dotnet/core/blob/master/Documentation/core-repos.md

* @dotnet-bot commands https://github.com/Microsoft/ChakraCore/wiki/Jenkins-CI-Checks

* up-for-grabs issue query https://github.com/dotnet/corefx/issues?q=is%3Aissue+is%3Aopen+label%3Aup-for-grabs

* area owner https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/issue-guide.md#areas

* build x86 Release .Net Desktop (https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/developer-guide.md#build)

```
build.cmd -buildArch=x86 -framework=netfx -release
```

* rebuild/test lib

https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/developer-guide.md#tests  

https://msdn.microsoft.com/en-us/library/ms164311.aspx MSBuild Command-Line Reference

```
msbuild /v:m /t:RebuildAndTest "/p:XunitOptions=-trait MyTrait=MyTrait"  System.Runtime.Extensions.Tests.csproj
msbuild /v:m /t:RebuildAndTest "/p:XunitOptions=-method System.IO.Tests.PathTests.Try_GetTempPath_Default"  System.Runtime.Extensions.Tests.csproj
```
* trait dotnet test https://github.com/Microsoft/vstest-docs/blob/master/docs/filter.md
```
dotnet test --filter TraitName=TraitValue
```

* test private CoreCLR change 

```
build.cmd -- /p:CoreCLROverridePath=d:\git\coreclr\bin\Product\Windows_NT.x64.Release\

```


https://github.com/dotnet/coreclr/pull/16151#issuecomment-362356957

https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/developer-guide.md#testing-with-private-coreclr-bits

https://github.com/dotnet/coreclr/blob/master/Documentation/workflow/UsingYourBuild.md

* Debugging NETFX tests in Visual Studio https://github.com/dotnet/corefx/blob/a7f6f470cb2c4cdaafdc3ad85e2520992a8db265/Documentation/building/windows-instructions.md#debugging-netfx-tests-in-visual-studio

* single test msbuild https://github.com/dotnet/corefx/wiki/Build-and-run-tests#run-and-debug-single-test-in-command-line

Extra

* Find string on source
```
  cmd -> findstr /n /s /c:"!Directory.Exists("  *.cs  
  bash -> git grep -n 'JsonDataContractCriticalHelper' | grep -v tests | cut -d: -f1,2 
```
