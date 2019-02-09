using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Test();
            Test2();
        }

        public static void Test2()
        {
            System.Collections.Generic2.Dictionary<string, string> dictionary;
            
            int currentCapacity = 3;

            // assert capacity remains the same when ensuring a capacity smaller or equal than existing
            for (int i = 0; i <= currentCapacity; i++)
            {
                dictionary = new System.Collections.Generic2.Dictionary<string, string>(currentCapacity);
                Debug.Assert(currentCapacity == dictionary.EnsureCapacity(i));
            }
        }

        public static void Test()
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
            for (int i = 0; i < 5; i++)
            {
                // Console.WriteLine(i);
                var key = Guid.NewGuid().ToString("D");
                keys.Add(key);
                test.Add(key, key);
                Debug.Assert(test[key] == key);
            }

            Debug.Assert(keys.Count == test.Count);

            foreach (KeyValuePair<string, string> item in System.Linq.Enumerable.ToArray(test))
            {
                test.Remove(item.Key);
            }

            Debug.Assert(test.Count == 0);
        }

    }
}
