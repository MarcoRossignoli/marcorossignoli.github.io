# marcorossignoli.github.io

## Build/Test


https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/developer-guide.md

Build Pivot https://github.com/dotnet/corefx/blob/6cb23ac20696ab314d2b28e95af40c8454bd7c0d/Documentation/coding-guidelines/project-guidelines.md#build-pivots

https://msdn.microsoft.com/en-us/library/ms164311.aspx MSBuild Command-Line Reference

```
msbuild /v:m /t:RebuildAndTest "/p:XunitOptions=-trait MyTrait=MyTrait"  System.Runtime.Extensions.Tests.csproj
msbuild /v:m /t:RebuildAndTest "/p:XunitOptions=-method System.IO.Tests.PathTests.Try_GetTempPath_Default"  System.Runtime.Extensions.Tests.csproj
```

Single test msbuild /p:XunitMethodName https://github.com/dotnet/corefx/wiki/Build-and-run-tests#run-and-debug-single-test-in-command-line

trait dotnet test https://github.com/Microsoft/vstest-docs/blob/master/docs/filter.md
```
dotnet test --filter TraitName=TraitValue
```

Debugging NETFX tests in Visual Studio https://github.com/dotnet/corefx/blob/a7f6f470cb2c4cdaafdc3ad85e2520992a8db265/Documentation/building/windows-instructions.md#debugging-netfx-tests-in-visual-studio  

[MsBuild Command line for switches](https://msdn.microsoft.com/en-us/library/ms164311.aspx)  

[XUnit method with msbuild](https://github.com/dotnet/buildtools/blob/master/Documentation/test-targets-usage.md#run-a-single-xunit-method)  

## Coverage

Workflow https://github.com/dotnet/corefx/issues/23588#issuecomment-394055817

## Benchmarking

https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/benchmarking.md  
corefx guide(my [PR](https://github.com/dotnet/coreclr/pull/18524#issuecomment-398237008)) https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/performance-tests.md  
adamsitnik https://github.com/dotnet/corefxlab/pull/2369#issuecomment-399122942

### Use corerun.exe

From Viktor Hofer way https://github.com/dotnet/corefx/pull/30632#issuecomment-399778513

From release build folder(after ..corefx\build -release) ..\corefx\bin\runtime\netcoreapp-Windows_NT-Release-x64  
Copy BenchmarkDotNet.Core to release build folder from https://www.nuget.org/packages/BenchmarkDotNet.Core/  
Copy BenchmarkDotNet.dll to release budil folder from https://www.nuget.org/packages/BenchmarkDotNet/  
Add test in Program.cs
```cs
 ...
 class MainConfig : ManualConfig
    {
        public MainConfig()
        {
            // Job #1
            // Add(Job.Default
            //     .With(Runtime.Core)
            //     .With(CsProjCoreToolchain.From(NetCoreAppSettings
            //         .NetCoreApp21
            //         .WithCustomDotNetCliPath(@"C:\dotnet\dotnet.exe", "OutOfProcessToolchain")))
            //     .WithId(".NET Core 2.1 master"));

            // Job #2
            Add(Job.Default
                .With(InProcessToolchain.Instance)
                 .With(Runtime.Core)
                 .WithId(".NET Core 2.1 regex"));

            Add(DefaultColumnProviders.Instance);
            Add(MarkdownExporter.GitHub);
            Add(new ConsoleLogger());
            Add(new HtmlExporter());
            Add(MemoryDiagnoser.Default);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new RegexBenchmark().RegexCtor();
            BenchmarkRunner.Run<RegexBenchmark>(new MainConfig());
        }
    }
```
Compile and run benchmarks from From release build folder
```
corerun ..\..\..\tools\csc.dll /noconfig /noconfig /optimize /r:System.Private.Corelib.dll /r:System.Runtime.dll /r:System.Runtime.Extensions.dll /r:System.Console.dll /r:System.Text.RegularExpressions.dll /r:System.Collections.dll /r:BenchmarkDotNet.dll /r:BenchmarkDotNet.Core.dll  /out:Program.dll Program.cs
corerun Program.dll
```

Change code recompile lib and re-run Program.dll 
```
? msbuild /t:rebuild /v:m /p:ConfigurationGroup=Release
Microsoft (R) Build Engine version 15.7.179.6572 for .NET Framework
Copyright (C) Microsoft Corporation. All rights reserved.

  System.Text.RegularExpressions -> ..corefx\bin\AnyOS.AnyCPU.Release\System.Text.RegularExpressions\netcoreapp\System.Text.RegularExpressions.dll
```

Build should move compilation to release build folder.

## Extras

* Find string on source
```
  cmd -> findstr /n /s /c:"!Directory.Exists("  *.cs  
  bash -> git grep -n 'JsonDataContractCriticalHelper' | grep -v tests | cut -d: -f1,2 
```

* dotnet core repos https://github.com/dotnet/core/blob/master/Documentation/core-repos.md
* area owner https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/issue-guide.md#areas



