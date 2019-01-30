IF EXIST c:\git\performance\scripts\benchmarks_ci.py (
c:\git\performance\scripts\benchmarks_ci.py --frameworks netcoreapp3.0 --filter System.Collections*.Dictionary* *.Perf_Dictionary.* --corerun C:\git\corefxupstream\artifacts\bin\testhost\netcoreapp-Windows_NT-Release-x64\shared\Microsoft.NETCore.App\9.9.9\CoreRun.exe C:\git\corefx\artifacts\bin\testhost\netcoreapp-Windows_NT-Release-x64\shared\Microsoft.NETCore.App\9.9.9\CoreRun.exe --bdn-arguments="--join"
)

IF EXIST d:\git\performance\scripts\benchmarks_ci.py (
d:\git\performance\scripts\benchmarks_ci.py --frameworks netcoreapp3.0 --filter System.Collections*.Dictionary* *.Perf_Dictionary.* --corerun C:\git\corefxupstream\artifacts\bin\testhost\netcoreapp-Windows_NT-Release-x64\shared\Microsoft.NETCore.App\9.9.9\CoreRun.exe C:\git\corefx\artifacts\bin\testhost\netcoreapp-Windows_NT-Release-x64\shared\Microsoft.NETCore.App\9.9.9\CoreRun.exe --bdn-arguments="--join"
)


