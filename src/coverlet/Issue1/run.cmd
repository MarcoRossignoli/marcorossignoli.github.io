del "test-results\coverage\coverage.json"
dotnet test "test\Keys.Api.Tests\Keys.Api.Tests.csproj" /p:CollectCoverage=true /p:CoverletOutput=..\..\test-results\  /p:MergeWith=..\..\test-results\coverage.json 
dotnet test "test\Keys.Application.Tests\Keys.Application.Tests.csproj" /p:CollectCoverage=true /p:CoverletOutput=..\..\test-results\  /p:MergeWith=..\..\test-results\coverage.json 
dotnet test "test\Keys.Data.Tests\Keys.Data.Tests.csproj" /p:CollectCoverage=true /p:CoverletOutput=..\..\test-results\ /p:MergeWith=..\..\test-results\coverage.json 
