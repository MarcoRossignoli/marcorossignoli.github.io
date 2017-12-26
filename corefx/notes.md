# marcorossignoli.github.io

* WIP<br>
  * https://github.com/dotnet/corefx/issues/23748 Don't directly throw Exception <br/>
  * https://github.com/dotnet/corefx/pull/25211 more precise error messages [here](https://github.com/MarcoRossignoli/marcorossignoli.github.io/blob/master/corefx/issue_25211.txt)
    waiting for Ubuntu debugging issue 

* @dotnet-bot commands https://github.com/Microsoft/ChakraCore/wiki/Jenkins-CI-Checks

* Contributing guide: 
https://github.com/dotnet/corefx/wiki/New-contributor-Docs#contributing-guide

* Dev guide

  https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/developer-guide.md
  
  Project guidelines https://github.com/dotnet/corefx/blob/master/Documentation/coding-guidelines/project-guidelines.md


* Easy issue: https://github.com/dotnet/corefx/labels/easy

* Code coverage https://github.com/dotnet/corefx/blob/master/Documentation/building/code-coverage.md#user-content-code-coverage-with-mscorlib-code

* area owner https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/issue-guide.md#areas

* build x86 Release .Net Desktop (https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/developer-guide.md#build)

  build.cmd -buildArch=x86 -framework=netfx -release
  
* test lib https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/developer-guide.md#tests

* trait dotnet test https://github.com/Microsoft/vstest-docs/blob/master/docs/filter.md

  dotnet test --filter TraitName=TraitValue

Extra

* Find string on source

  findstr /n /s /c:"!Directory.Exists("  *.cs
