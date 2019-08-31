using System;
using System.Collections.Generic;
using System.Text;

namespace Interview
{
    public class ArrayAndStrings
    {
        public static void IsUnique1_1()
        {
            Console.WriteLine(IsUnique(""));
            Console.WriteLine(IsUnique("A"));
            Console.WriteLine(IsUnique(null));
            Console.WriteLine(IsUnique("ABC"));
            Console.WriteLine(IsUnique("ABBC"));

            return;

            static bool IsUnique(string s)
            {
                if (s is null || s.Length <= 1)
                    return true;

                for (int i = 0; i < s.Length; i++)
                {
                    for (int k = 0; k < s.Length; k++)
                    {
                        if (i == k)
                            continue;

                        if (s[i] == s[k])
                            return false;
                    }
                }

                return true;
            }

        }
    }
}
