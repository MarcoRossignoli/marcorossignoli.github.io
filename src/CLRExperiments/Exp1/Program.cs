using System;
namespace Exp1
{
    // https://github.com/dotnet/coreclr/blob/master/Documentation/building/debugging-instructions.md
    // https://github.com/dotnet/coreclr/blob/master/Documentation/botr/ryujit-overview.md
    // https://github.com/dotnet/coreclr/blob/master/Documentation/botr/ryujit-overview.md#phases-of-ryujit
    class Program
    {
        static void Main(string[] args)
        {
            DoubleNegation.I1(new Random().Next());
            DoubleNegation.M1(new Random().Next());
        }
    }
}
