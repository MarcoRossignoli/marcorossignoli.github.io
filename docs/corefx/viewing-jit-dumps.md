# View JIT Dump

Guide reference https://github.com/dotnet/coreclr/blob/master/Documentation/building/viewing-jit-dumps.md

1) Build CoreClr in Release and in Debug
```
build.cmd -skiptests
build.cmd -release -skiptests
```

2) Create console project
```cs
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp3.0</TargetFramework>
    <RuntimeIdentifier>win-x64</RuntimeIdentifier>
  </PropertyGroup>

</Project>
...
namespace CoreClrJitAsm
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            Test(dic, 1);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void Test(Dictionary<int, int> dic, int v)
        {
            dic.ContainsValue(v);
        }
    }
}

```
3) Publish console App 
```
C:\Users\Marco\Downloads\Tmp\CoreClrJitAsm
λ dotnet publish -c Release
```
4) Copy files from local CoreClr
```
robocopy /e c:\git\coreclr\bin\Product\Windows_NT.x64.Release C:\Users\Marco\Downloads\Tmp\CoreClrJitAsm\bin\Release\netcoreapp3.0\win-x64\publish\
copy /y c:\git\coreclr\bin\Product\Windows_NT.x64.Debug\clrjit.dll C:\Users\Marco\Downloads\Tmp\CoreClrJitAsm\bin\Release\netcoreapp3.0\win-x64\publish\
```
5) Setup knobs
```
SET COMPlus_DumpJittedMethods=1 <- list jitted methods
SET COMPlus_JitDisasm=ContainsValue <- methods to dump comma separated
SET COMPlus_ReadyToRun=0 <- disable crossgen
SET COMPlus_TieredCompilation_Test_OptimizeTier0=1 <- enable Tier 1
```

6) Run app
```
C:\Users\Marco\Downloads\Tmp\CoreClrJitAsm
λ bin\Release\netcoreapp3.0\win-x64\publish\CoreClrJitAsm.exe
```
