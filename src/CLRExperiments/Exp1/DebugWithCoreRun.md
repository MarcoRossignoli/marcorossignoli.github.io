### DEBUG with CoreRun.exe

https://github.com/dotnet/coreclr/blob/master/Documentation/building/debugging-instructions.md   


VS options
![File](file.png)

Command: `$(SolutionDir)..\..\product\Windows_NT.$(Platform).$(Configuration)\corerun.exe`  
Command Arguments: `...\Exp1.dll /v`  <-- .net main dll to debug
WorkingDirectory: `$(SolutionDir)..\..\product\Windows_NT.$(Platform).$(Configuration)`
