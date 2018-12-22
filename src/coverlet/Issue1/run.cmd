rem dotnet test --configuration Release "test\Keys.Api.Tests\Keys.Api.Tests.csproj" /p:CollectCoverage=true /p:CoverletOutputFormat=opencover  /p:CoverletOutput=..\..\test-results\coverage\Keys.Api.Tests.xml
rem dotnet test "test\Keys.Application.Tests\Keys.Application.Tests.csproj" /p:CollectCoverage=true /p:CoverletOutputFormat=opencover  /p:CoverletOutput=..\..\test-results\coverage\Keys.Application.Tests.xml /p:Include="[Keys.Application]*"
rem dotnet test "test\Keys.Application.Tests\Keys.Application.Tests.csproj" /p:CollectCoverage=true   /p:CoverletOutput=..\..\test-results\coverage\Keys.Application.Tests.json /p:Include="[Keys.Application]*"
rem test "test\Keys.Data.Tests\Keys.Data.Tests.csproj" /p:CollectCoverage=true /p:CoverletOutputFormat=opencover  /p:CoverletOutput=..\..\test-results\coverage\Keys.Data.Tests.xml

rem reportgenerator -reports:.\test-results\coverage\*.xml -targetdir:.\test-results\report


rem dotnet test --configuration Release "test\Keys.Api.Tests\Keys.Api.Tests.csproj" /p:CollectCoverage=true  /p:CoverletOutput=..\..\test-results\coverage\Keys.Api.Tests.json 
rem dotnet test --configuration Release "test\Keys.Application.Tests\Keys.Application.Tests.csproj" /p:CollectCoverage=true  /p:CoverletOutput=..\..\test-results\coverage\Keys.Application.Tests.json 
rem dotnet test --configuration Release "test\Keys.Data.Tests\Keys.Data.Tests.csproj" /p:CollectCoverage=true  /p:CoverletOutput=..\..\test-results\coverage\Keys.Data.Tests.json 

rem dotnet test --configuration Release  /p:CollectCoverage=true /p:CoverletOutput=..\..\test-results\coverage /p:MergeWith='..\..\test-results\coverage\result.json'
rem reportgenerator -reports:.\test-results\coverage\*.json -targetdir:.\test-results\report

del "D:\git\marcorossignoli.github.io\src\coverlet\Issue1\test-results\coverage\coverage.json" /Y
dotnet test "D:\git\marcorossignoli.github.io\src\coverlet\Issue1\test\Keys.Api.Tests\Keys.Api.Tests.csproj" /p:CollectCoverage=true /p:CoverletOutput=..\..\test-results\coverage\ /p:MergeWith=..\..\test-results\coverage\coverage.json
dotnet test "D:\git\marcorossignoli.github.io\src\coverlet\Issue1\test\Keys.Application.Tests\Keys.Application.Tests.csproj" /p:CollectCoverage=true /p:CoverletOutput=..\..\test-results\coverage\ /p:MergeWith=..\..\test-results\coverage\coverage.json
