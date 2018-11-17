# marcorossignoli.github.io

## Build/Test


https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/developer-guide.md

Build Pivot https://github.com/dotnet/corefx/blob/6cb23ac20696ab314d2b28e95af40c8454bd7c0d/Documentation/coding-guidelines/project-guidelines.md#build-pivots

https://msdn.microsoft.com/en-us/library/ms164311.aspx MSBuild Command-Line Reference

```
msbuild /v:m /t:RebuildAndTest "/p:XunitOptions=-trait MyTrait=MyTrait"  System.Runtime.Extensions.Tests.csproj
msbuild /v:m /t:RebuildAndTest "/p:XunitOptions=-method System.IO.Tests.PathTests.Try_GetTempPath_Default"  System.Runtime.Extensions.Tests.csproj  
msbuild /v:m /t:RebuildAndTest "/p:XunitOptions=-method *Impersonate_WindowsIdentity_Object_InvalidToken"
```

Single test msbuild /p:XunitMethodName https://github.com/dotnet/corefx/wiki/Build-and-run-tests#run-and-debug-single-test-in-command-line

trait dotnet test https://github.com/Microsoft/vstest-docs/blob/master/docs/filter.md
```
dotnet test --filter TraitName=TraitValue
```

Debugging NETFX tests in Visual Studio https://github.com/dotnet/corefx/blob/a7f6f470cb2c4cdaafdc3ad85e2520992a8db265/Documentation/building/windows-instructions.md#debugging-netfx-tests-in-visual-studio  

[MsBuild Command line for switches](https://msdn.microsoft.com/en-us/library/ms164311.aspx)  

[XUnit method with msbuild](https://github.com/dotnet/buildtools/blob/master/Documentation/test-targets-usage.md#run-a-single-xunit-method)  

## Coverage

Workflow https://github.com/dotnet/corefx/issues/23588#issuecomment-394055817

On CoreClr repo:
```
build -release -skipTests -- /p:DebugType=pdbonly
build -release -skipTests -windowsmscorlib -- /p:DebugType=pdbonly
```

The second step is important to get IL only of CoreLib on the folder that is going to be used in the CoreCLROverridePath, but as mentioned can be done in other ways.

On CoreFx repo:
```
build -- /p:CoreCLROverridePath=C:\s\coreclr\bin\Product\Windows_NT.x64.Release
build-tests -skipTests -- /p:CoreCLROverridePath=C:\s\coreclr\bin\Product\Windows_NT.x64.Release
cd src\System.Collections\tests
msbuild /t:RebuildAndTest /p:Coverage=True /p:CodeCoverageAssemblies="System.Private.CoreLib"
```

## Benchmarking

https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/benchmarking.md  
Adam Sitnik https://github.com/dotnet/corefxlab/pull/2369#issuecomment-399122942  
Andrey Akinshin https://github.com/dotnet/corefx/pull/32389#issuecomment-424642336  
Viktor Hofer https://github.com/dotnet/corefx/pull/30632#issuecomment-399778513  
corefx guide(my [PR](https://github.com/dotnet/coreclr/pull/18524#issuecomment-398237008)) https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/performance-tests.md  

## Extras

* Find string on source
```
  cmd -> findstr /n /s /c:"!Directory.Exists("  *.cs  
  bash -> git grep -n 'JsonDataContractCriticalHelper' | grep -v tests | cut -d: -f1,2 
```

* dotnet core repos https://github.com/dotnet/core/blob/master/Documentation/core-repos.md
* area owner https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/issue-guide.md#areas
* [MSBuild-Tips-&-Tricks.md](https://github.com/Microsoft/msbuild/blob/b657647d2e6f4ed20ce6cb3201a55ee02f09c062/documentation/wiki/MSBuild-Tips-%26-Tricks.md)
* dotnet core release csv/json https://github.com/dotnet/core/tree/master/release-notes
* dotnet core SDKs https://github.com/dotnet/core-sdk#installers-and-binaries  
* https://github.com/dotnet/platform-compat platform-compat

