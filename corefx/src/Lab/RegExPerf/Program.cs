using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CustomCoreClr;
using System;
using System.IO;
using System.Reflection;

namespace RegExPerf
{
    class Program
    {
        static void Main(string[] args)
        {
            CompareBeforeAfter(args);
        }

        static void CompareBeforeAfter(string[] args)
        {
            string packagesPath = Path.GetFullPath("../../../../../../../../corefx/bin/packages/Release");
            string version =
                Directory.GetFiles(packagesPath, "Microsoft.Private.CoreFx*")[0].TrimEnd(".nupkg".ToCharArray()).Substring(
                Directory.GetFiles(packagesPath, "Microsoft.Private.CoreFx*")[0].TrimEnd(".nupkg".ToCharArray())
                .IndexOf("Microsoft.Private.CoreFx.NETCoreApp"))
                .Replace("Microsoft.Private.CoreFx.NETCoreApp.", "");

            var libPath = Path.GetFullPath("../../../../../../../../corefx/bin") +
                @"\AnyOS.AnyCPU.Release\System.Text.RegularExpressions\netcoreapp\System.Text.RegularExpressions.dll";

            if (!File.Exists(libPath))
            {
                Console.WriteLine(libPath + " not found");
                return;
            }

            Console.WriteLine("PackagesPath:" + packagesPath);
            Console.WriteLine("Version:" + version);
            Console.WriteLine("LibPath:" + libPath);



            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
             .Run(args, DefaultConfig.Instance
                 .With(Job.ShortRun
                     .With(CustomCoreClrToolchain.CreateForLocalCoreFxBuild(
                         pathToNuGetFolder: packagesPath,
                         privateCoreFxNetCoreAppVersion: version,
                         displayName: "before"))
                     .AsBaseline()
                     .WithId("before"))
                 .With(Job.ShortRun
                     .With(CustomCoreClrToolchain.CreateForLocalCoreFxBuild(
                         pathToNuGetFolder: packagesPath,
                         privateCoreFxNetCoreAppVersion: version,
                         displayName: "after",
                         filesToCopy: new[] { libPath }
                         ))
                     .WithId("after"))
                 .With(MarkdownExporter.GitHub)
                 //.With(new ConsoleLogger())
                 //.With(new HtmlExporter())
                 .With(MemoryDiagnoser.Default)
                 .KeepBenchmarkFiles());

        }

    }
}
