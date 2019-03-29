Generate instrumented module  
```
dotnet msbuild /t:Restore,Build,InstrumentModulesAfterBuild /p:CollectCoverage=true "C:\git\coverlet-issue-343\DemoLibraryTests\DemoLibraryTests.csproj"
```
