# marcorossignoli.github.io

* WIP<br>
  * https://github.com/dotnet/corefx/issues/25403 Perflib sync
  
* dotnet core repos https://github.com/dotnet/core/blob/master/Documentation/core-repos.md

* @dotnet-bot commands https://github.com/Microsoft/ChakraCore/wiki/Jenkins-CI-Checks

* Issue query: 

https://github.com/dotnet/corefx/issues?q=is%3Aissue+is%3Aopen+label%3Aup-for-grabs up for grabs

* area owner https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/issue-guide.md#areas

* build x86 Release .Net Desktop (https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/developer-guide.md#build)

```
build.cmd -buildArch=x86 -framework=netfx -release
```

* rebuild/test lib https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/developer-guide.md#tests
```
msbuild System.Net.NameResolution.sln /t:RebuildAndTest /verbosity:minimal
```
* rebuild/test unix https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/developer-guide.md#platformspecificattribute
* trait dotnet test https://github.com/Microsoft/vstest-docs/blob/master/docs/filter.md
```
dotnet test --filter TraitName=TraitValue
```

* test private CoreCLR change https://github.com/dotnet/coreclr/pull/16151

Extra

* Find string on source
```
  cmd -> findstr /n /s /c:"!Directory.Exists("  *.cs  
  bash -> git grep -n 'JsonDataContractCriticalHelper' | grep -v tests | cut -d: -f1,2 
```
