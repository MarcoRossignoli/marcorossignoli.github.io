using System;
using System.Collections.Generic;
using System.Linq;

namespace Interview.ArrayAndString
{
    public class ArrayAndString_Exercise
    {
        public static void Permutation()
        {
            // total permutation n!
            Array.ForEach(GetPermutations("abc"), r => Console.WriteLine(r));

            return;

            static string[] GetPermutations(string str)
            {
                List<string> permutations = new List<string>();

                Permutation(str, "", permutations);

                return permutations.ToArray();
            }

            static void Permutation(string str, string prefix, List<string> list)
            {
                if (str.Length == 0)
                {
                    list.Add(prefix);
                }
                else
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        var rem = str.Substring(0, i) + str.Substring(i + 1);
                        Permutation(rem, prefix + str[i], list);
                    }
                }
            }
        }

        public static void IsUnique()
        {
            string[] str = new string[] { "abcdef", "abcdefa", "abcdefghilmnd" };

            foreach (string s in str)
            {
                Console.WriteLine(IsUnique(s));
            }

            return;

            static bool IsUnique(string str)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    for (int k = 0; k < str.Length; k++)
                    {
                        if (i == k)
                            continue;

                        if (str[i] == str[k])
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }
    }
}
