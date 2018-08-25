``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Core i7-3740QM CPU 2.70GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
Frequency=2628200 Hz, Resolution=380.4885 ns, Timer=TSC
.NET Core SDK=2.1.401
  [Host]     : .NET Core 2.1.3 (CoreCLR 4.6.26725.06, CoreFX 4.6.26725.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.3 (CoreCLR 4.6.26725.06, CoreFX 4.6.26725.05), 64bit RyuJIT


```
|    Method |     Mean |     Error |    StdDev |      Gen 0 | Allocated |
|---------- |---------:|----------:|----------:|-----------:|----------:|
| RegexCtor | 159.4 ms | 0.6307 ms | 0.5900 ms | 38000.0000 | 114.73 MB |
