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
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).RunAll();
            //   .Run(args, DefaultConfig.Instance.With(
            //       Job.ShortRun.With(
            //           new CoreRunToolchain(
            //               new FileInfo(@"D:\git\corefx\bin\testhost\netcoreapp-Windows_NT-Release-x64\shared\Microsoft.NETCore.App\9.9.9\CoreRun.exe")
            //               ))));
        }
    }
}
