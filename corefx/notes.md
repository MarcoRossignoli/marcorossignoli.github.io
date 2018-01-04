# marcorossignoli.github.io

* WIP<br>
  * https://github.com/dotnet/corefx/issues/23748 Don't directly throw Exception 
  * https://github.com/dotnet/corefx/issues/25403 Perflib sync
  
* @dotnet-bot commands https://github.com/Microsoft/ChakraCore/wiki/Jenkins-CI-Checks

* Contributing guide: 
https://github.com/dotnet/corefx/wiki/New-contributor-Docs#contributing-guide

* Dev guide

  https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/developer-guide.md
  
  Project guidelines https://github.com/dotnet/corefx/blob/master/Documentation/coding-guidelines/project-guidelines.md


* Issue query: 

https://github.com/dotnet/corefx/issues?q=is%3Aissue+is%3Aopen+label%3Aup-for-grabs up for grabs

https://github.com/dotnet/corefx/issues?utf8=%E2%9C%93&q=is%3Aissue+is%3Aopen+label%3Aup-for-grabs+label%3Aeasy easy


* Code coverage https://github.com/dotnet/corefx/blob/master/Documentation/building/code-coverage.md#user-content-code-coverage-with-mscorlib-code

* area owner https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/issue-guide.md#areas

* build x86 Release .Net Desktop (https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/developer-guide.md#build)

  build.cmd -buildArch=x86 -framework=netfx -release
  
* test lib https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/developer-guide.md#tests

* trait dotnet test https://github.com/Microsoft/vstest-docs/blob/master/docs/filter.md

  dotnet test --filter TraitName=TraitValue

Extra

* Find string on source

  cmd findstr /n /s /c:"!Directory.Exists("  *.cs
  
  bash git grep -n 'JsonDataContractCriticalHelper' | grep -v tests | cut -d: -f1,2 
