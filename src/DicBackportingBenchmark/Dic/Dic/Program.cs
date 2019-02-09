using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            System.Collections.Generic2.Dictionary<string, string> test = new System.Collections.Generic2.Dictionary<string, string>();

            Debug.Assert(test.Count == 0);
            Debug.Assert(System.Linq.Enumerable.ToArray(test).Length == 0);

            foreach (KeyValuePair<string, string> item in test)
            {
                Debug.Assert(false);
            }

            try
            {
                var key = test["Key"];
                Debug.Assert(false);
            }
            catch (KeyNotFoundException)
            {

            }

            List<string> keys = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                // Console.WriteLine(i);
                var key = Guid.NewGuid().ToString("D");
                keys.Add(key);
                test.Add(key, key);
                Debug.Assert(test[key] == key);
            }

            Debug.Assert(keys.Count == test.Count);

            foreach (var item in keys)
            {
                Debug.Assert(test[item] == item);
            }

            foreach (KeyValuePair<string, string> item in System.Linq.Enumerable.ToArray(test))
            {
                test.Remove(item.Key);
            }

            foreach (KeyValuePair<string, string> item in System.Linq.Enumerable.ToArray(test))
            {
                test.Remove(item.Key);
            }

            Debug.Assert(test.Count == 0);
        }
    }
}
