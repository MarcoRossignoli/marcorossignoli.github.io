SET COMPlus_JitDisasm=Check CheckOptimized
SET COMPlus_JitDump=
SET COMPlus_TieredCompilation_Test_OptimizeTier0=1
SET COMPlus_JitDiffableDasm=1
dotnet publish -c release
robocopy /e %~d0\git\coreclr\bin\Product\Windows_NT.x64.Release %~dp0bin\Release\netcoreapp3.0\win-x64\publish\ > nul
copy /y %~d0\git\coreclr\bin\Product\Windows_NT.x64.Debug\clrjit.dll %~dp0bin\Release\netcoreapp3.0\win-x64\publish\
%~dp0bin\Release\netcoreapp3.0\win-x64\publish\Exp1.exe > %~dp0bin\Release\netcoreapp3.0\win-x64\publish\jitdasm.txt
start notepad.exe %~dp0bin\Release\netcoreapp3.0\win-x64\publish\jitdasm.txt


SET COMPlus_JitDisasm=
SET COMPlus_JitDump=Check CheckOptimized
SET COMPlus_TieredCompilation_Test_OptimizeTier0=1
SET COMPlus_JitDiffableDasm=1
echo SET JITMinOpts=1
dotnet publish -c release
robocopy /e %~d0\git\coreclr\bin\Product\Windows_NT.x64.Release %~dp0bin\Release\netcoreapp3.0\win-x64\publish\ > nul
copy /y %~d0\git\coreclr\bin\Product\Windows_NT.x64.Debug\clrjit.dll %~dp0bin\Release\netcoreapp3.0\win-x64\publish\
%~dp0bin\Release\netcoreapp3.0\win-x64\publish\Exp1.exe > %~dp0bin\Release\netcoreapp3.0\win-x64\publish\jitdump.txt
start notepad.exe %~dp0bin\Release\netcoreapp3.0\win-x64\publish\jitdump.txt
