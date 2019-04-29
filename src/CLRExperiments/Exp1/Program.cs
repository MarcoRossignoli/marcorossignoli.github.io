using System;

namespace Clr
{
    // https://github.com/dotnet/coreclr/blob/master/Documentation/building/debugging-instructions.md
    // https://github.com/dotnet/coreclr/blob/master/Documentation/botr/ryujit-overview.md
    class Program
    {
        static void Main(string[] args)
        {
            MyTest mt = new MyTest();
            Console.WriteLine(mt.Check(new string[5], 6));
        }
    }
}
