dotnet test "test\Keys.Api.Tests\Keys.Api.Tests.csproj" /p:CollectCoverage=true /p:CoverletOutputFormat=opencover  /p:CoverletOutput=..\..\test-results\coverage\Keys.Api.Tests.xml
#dotnet test "test\Keys.Application.Tests\Keys.Application.Tests.csproj" /p:CollectCoverage=true /p:CoverletOutputFormat=opencover  /p:CoverletOutput=..\..\test-results\coverage\Keys.Application.Tests.xml
#dotnet test "test\Keys.Data.Tests\Keys.Data.Tests.csproj" /p:CollectCoverage=true /p:CoverletOutputFormat=opencover  /p:CoverletOutput=..\..\test-results\coverage\Keys.Data.Tests.xml

reportgenerator -reports:.\test-results\coverage\*.xml -targetdir:.\test-results\report