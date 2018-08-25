``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Core i7-3740QM CPU 2.70GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
Frequency=2628200 Hz, Resolution=380.4885 ns, Timer=TSC
.NET Core SDK=2.1.400
  [Host]     : .NET Core 2.1.2 (CoreCLR 4.6.26628.05, CoreFX 4.6.26629.01), 64bit RyuJIT
  Job-CUBZAX : .NET Core 9086cdb4-84d3-4c7f-a7c6-da44695d368b (CoreCLR 4.6.26820.04, CoreFX 4.6.26920.0), 64bit RyuJIT
  ShortRun   : .NET Core 32f16950-b50a-4f36-b759-a277438c2802 (CoreCLR 4.6.26820.04, CoreFX 4.6.26920.0), 64bit RyuJIT

Runtime=Core  Toolchain=CoreRun  

```
|    Method |      Job | IterationCount | LaunchCount | WarmupCount |     Mean |     Error |   StdDev |      Gen 0 | Allocated |
|---------- |--------- |--------------- |------------ |------------ |---------:|----------:|---------:|-----------:|----------:|
| RegexCtor |  Default |        Default |     Default |     Default | 350.7 ms |  1.264 ms | 1.120 ms | 35000.0000 |  106.8 MB |
| RegexCtor | ShortRun |              3 |           1 |           3 | 351.6 ms | 22.005 ms | 1.243 ms | 35000.0000 |  106.8 MB |
