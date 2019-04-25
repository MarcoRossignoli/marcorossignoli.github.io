SET COMPlus_JitDisasm=Check
SET COMPlus_TieredCompilation_Test_OptimizeTier0=1
SET COMPlus_JitDiffableDasm=1
dotnet publish -c release
copy /y %~d0\git\coreclr\bin\Product\Windows_NT.x64.Debug\clrjit.dll %~dp0bin\Release\netcoreapp3.0\win-x64\publish\
%~dp0bin\Release\netcoreapp3.0\win-x64\publish\Exp1.exe