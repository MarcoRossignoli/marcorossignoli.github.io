using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CoreRun;
using System.IO;

namespace RegExPerf
{
    class Program
    {
        static void Main(string[] args)
        {
            // dotnet run -c Release -f netcoreapp2.1 -- -f *Benchmarks* --coreRun "D:\git\corefx\bin\testhost\netcoreapp-Windows_NT-Release-x64\shared\Microsoft.NETCore.App\9.9.9\CoreRun.exe"

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

            //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
            //   .Run(args, DefaultConfig.Instance.With(
            //       Job.ShortRun.With(
            //           new CoreRunToolchain(
            //               new FileInfo(@"D:\git\corefx\bin\testhost\netcoreapp-Windows_NT-Release-x64\shared\Microsoft.NETCore.App\9.9.9\CoreRun.exe")
            //               ))));


        }
    }
}
