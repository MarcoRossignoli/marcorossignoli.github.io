SET COMPlus_JitDisasm=M1 I1
SET COMPlus_JitDump=
SET COMPlus_JitDiffableDasm=1
SET COMPlus_TieredCompilation=1
dotnet restore
dotnet publish -c release
robocopy /e %~d0\git\coreclr\bin\Product\Windows_NT.x64.Release %~dp0bin\Release\netcoreapp3.0\win-x64\publish\ > nul
copy /y %~d0\git\coreclr\bin\Product\Windows_NT.x64.Debug\clrjit.dll %~dp0bin\Release\netcoreapp3.0\win-x64\publish\
%~dp0bin\Release\netcoreapp3.0\win-x64\publish\Exp1.exe > %~dp0bin\Release\netcoreapp3.0\win-x64\publish\jitdasm.txt
start notepad.exe %~dp0bin\Release\netcoreapp3.0\win-x64\publish\jitdasm.txt

SET COMPlus_JitDisasm=
SET COMPlus_JitDump=M1 I1
SET COMPlus_JitDiffableDasm=1
SET COMPlus_TieredCompilation=1
dotnet restore
dotnet publish -c release
robocopy /e %~d0\git\coreclr\bin\Product\Windows_NT.x64.Release %~dp0bin\Release\netcoreapp3.0\win-x64\publish\ > nul
copy /y %~d0\git\coreclr\bin\Product\Windows_NT.x64.Debug\clrjit.dll %~dp0bin\Release\netcoreapp3.0\win-x64\publish\
%~dp0bin\Release\netcoreapp3.0\win-x64\publish\Exp1.exe > %~dp0bin\Release\netcoreapp3.0\win-x64\publish\jitdasm.txt
start notepad.exe %~dp0bin\Release\netcoreapp3.0\win-x64\publish\jitdasm.txt


