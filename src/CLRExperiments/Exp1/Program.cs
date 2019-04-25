using System;

namespace Clr
{
    class Program
    {
        static void Main(string[] args)
        {
            MyTest mt = new MyTest();
            Console.WriteLine(mt.Check(new string[5], 6));
        }
    }
}
