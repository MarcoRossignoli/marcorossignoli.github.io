using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace BenchmarksPlayground
{
    [MemoryDiagnoser]
    [DisassemblyDiagnoser]
    public class OutRefMutability
    {
        [Benchmark]
        public void TestChangeValueRefEveryCycle()
        {
            int value = 10;
            ChangeValueRefEveryCycle(ref value);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ChangeValueRefEveryCycle(ref int value)
        {
            while (value > 0)
            {
                value--;
            }
        }

        [Benchmark]
        public void TestChangeValueRefAtTheEnd()
        {
            int value = 10;
            ChangeValueRefAtTheEnd(ref value);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void ChangeValueRefAtTheEnd(ref int value)
        {
            int local = value;
            while (local > 0)
            {
                local--;
            }
            value = local;
        }
    }
}
