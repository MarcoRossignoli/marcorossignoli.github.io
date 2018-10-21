using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace BenchmarksPlayground
{
    [MemoryDiagnoser]
    [DisassemblyDiagnoser(printPrologAndEpilog: false)]
    public class VirtualCall
    {
        private NoVirtualCallClass _nonVirtual;
        private VirtualCallClass _virtual;

        public VirtualCall()
        {
            _nonVirtual = new NoVirtualCallClass();
            _virtual = new VirtualCallClass();
        }

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void NoVirtual()
        {
            _nonVirtual.Method();
        }

        [Benchmark]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Virtual()
        {
            _virtual.Method();
        }
    }

    public sealed class NoVirtualCallClass
    {
        private int _i = 0;
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Method()
        {
            _i++;
        }
    }

    public class VirtualCallClass
    {
        private int _i = 0;
        [MethodImpl(MethodImplOptions.NoInlining)]
        public virtual void Method()
        {
            _i++;
        }
    }
}
