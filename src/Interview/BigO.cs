using System;
using System.Collections.Generic;
using System.Text;

namespace Interview
{
    public static class BigO
    {
        public static void Permutation()
        {
            string val = "va";

            Permutation(val, "");

            return;

            void Permutation(string str, string prefix)
            {
                if (str.Length == 0)
                {
                    Console.WriteLine(prefix);
                }
                else
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        string rem = str.Substring(0, i) + str.Substring(i + 1);
                        Permutation(rem, prefix + str[i]);
                    }
                }
            }
        }
    }
}
