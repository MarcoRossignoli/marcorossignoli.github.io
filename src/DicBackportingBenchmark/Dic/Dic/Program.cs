using System;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            System.Collections.Generic2.Dictionary<string, string> test = new System.Collections.Generic2.Dictionary<string, string>();
            for (int i = 0; i < 100; i++)
            {
                var key = Guid.NewGuid().ToString("D");
                test.Add(key, "V_" + i);
                var r = test[key];
            }
        }
    }
}
