# marcorossignoli.github.io

## Build/Test

* rebuild/test lib

https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/developer-guide.md#tests  

https://msdn.microsoft.com/en-us/library/ms164311.aspx MSBuild Command-Line Reference

```
msbuild /v:m /t:RebuildAndTest "/p:XunitOptions=-trait MyTrait=MyTrait"  System.Runtime.Extensions.Tests.csproj
msbuild /v:m /t:RebuildAndTest "/p:XunitOptions=-method System.IO.Tests.PathTests.Try_GetTempPath_Default"  System.Runtime.Extensions.Tests.csproj
```

* build x86 Release .Net Desktop (https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/developer-guide.md#build)

```
build.cmd -buildArch=x86 -framework=netfx -release
```
* Running tests in a different target framework msbuild (es. netfx)  

https://github.com/dotnet/corefx/blob/8e842fa29e14694cca96c6e39a38199c55a3a02e/Documentation/project-docs/developer-guide.md#running-tests-in-a-different-target-framework


* trait dotnet test https://github.com/Microsoft/vstest-docs/blob/master/docs/filter.md
```
dotnet test --filter TraitName=TraitValue
```

* Build Pivot https://github.com/dotnet/corefx/blob/6cb23ac20696ab314d2b28e95af40c8454bd7c0d/Documentation/coding-guidelines/project-guidelines.md#build-pivots

* Debugging NETFX tests in Visual Studio https://github.com/dotnet/corefx/blob/a7f6f470cb2c4cdaafdc3ad85e2520992a8db265/Documentation/building/windows-instructions.md#debugging-netfx-tests-in-visual-studio

* single test msbuild https://github.com/dotnet/corefx/wiki/Build-and-run-tests#run-and-debug-single-test-in-command-line

## Test private CoreCLR change 

```
build.cmd -- /p:CoreCLROverridePath=d:\git\coreclr\bin\Product\Windows_NT.x64.Release\

```

PR sample https://github.com/dotnet/coreclr/pull/16151#issuecomment-362356957

https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/developer-guide.md#testing-with-private-coreclr-bits

https://github.com/dotnet/coreclr/blob/master/Documentation/workflow/UsingYourBuild.md#update-coreclr-from-raw-binary-output


## Coverage

Workflow https://github.com/dotnet/corefx/issues/23588#issuecomment-394055817

## Benchmarking

corefx guide([PR](https://github.com/dotnet/coreclr/pull/18524#issuecomment-398237008)) https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/performance-tests.md  
adamsitnik https://github.com/dotnet/corefxlab/pull/2369#issuecomment-399122942


##Extras

* Find string on source
```
  cmd -> findstr /n /s /c:"!Directory.Exists("  *.cs  
  bash -> git grep -n 'JsonDataContractCriticalHelper' | grep -v tests | cut -d: -f1,2 
```

* dotnet core repos https://github.com/dotnet/core/blob/master/Documentation/core-repos.md
* area owner https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/issue-guide.md#areas



