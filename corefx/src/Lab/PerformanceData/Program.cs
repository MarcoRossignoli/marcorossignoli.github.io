using System;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;
using System.IO;

namespace PerformanceData
{
    class Program
    {
        private static Guid providerId = new Guid("{51D1685C-35ED-45be-99FE-17261A4F27F3}");
        private static Guid typingCounterSetId = new Guid("{582803C9-AACD-45e5-8C30-571141A22092}");
        private static CounterSet typingCounterSet;         // Defines the counter set
        private static CounterSetInstance typingCsInstance;

        static void Main(string[] args)
        {
            var schemaPath = RegisterCounters();
            typingCounterSet = new CounterSet(providerId, typingCounterSetId, CounterSetInstanceType.Single);
            try
            {
                typingCounterSet.AddCounter(1, CounterType.Delta32, "Words Typed In Interval");
                typingCsInstance = typingCounterSet.CreateCounterSetInstance("Typing Instance");
                typingCsInstance.Counters[1].Value = 0;
                typingCsInstance.Counters["Words Typed In Interval"].Increment();

            }
            finally
            {
                typingCounterSet.Dispose();
                UnregisterCounters(schemaPath);
                Directory.Delete(Path.GetDirectoryName(schemaPath), true);
            }
        }

        static void UnregisterCounters(string manifestPathTmp)
        {
            var psi = new ProcessStartInfo();
            psi.FileName = "unlodctr";
            psi.Arguments = "/m:\"" + manifestPathTmp + "\"";
            psi.UseShellExecute = false;
            var process = Process.Start(psi);
            process.WaitForExit();
            System.Diagnostics.Debug.Assert(process.ExitCode == 0);
        }

        static string RegisterCounters()
        {
            var manifestPathTmp = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName(), "provider.man");
            Directory.CreateDirectory(Path.GetDirectoryName(manifestPathTmp));
            File.Copy("manifest\\provider.man", manifestPathTmp);
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "lodctr";
            psi.Arguments = "/m:\"" + manifestPathTmp + "\"";
            psi.UseShellExecute = false;
            var process = Process.Start(psi);
            process.WaitForExit();
            System.Diagnostics.Debug.Assert(process.ExitCode == 0);
            return manifestPathTmp;
        }

    }
}
