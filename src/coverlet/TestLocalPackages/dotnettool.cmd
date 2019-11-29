dotnet tool install coverlet.console --global --version 1.6.31-geaaf0afc31

dotnet build
coverlet XUnitTestProject1\bin\Debug\netcoreapp2.1\XUnitTestProject1.dll --target "dotnet" --targetargs "test XUnitTestProject1\XUnitTestProject1.csproj --no-build"

dotnet tool uninstall coverlet.console --global