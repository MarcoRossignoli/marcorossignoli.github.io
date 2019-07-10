using System;

namespace Interview
{
    public static class BigO
    {
        // https://www.geeksforgeeks.org/write-a-c-program-to-print-all-permutations-of-a-given-string/
        public static void Permutation()
        {
            string val = "va";

            Permutation(val, "");

            return;

            static void Permutation(string str, string prefix)
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

        public static void FibonacciNth()
        {
            Console.WriteLine(Fib(4));

            return;

            static int Fib(int n)
            {
                if (n <= 1)
                    return n;

                return Fib(n - 1) + Fib(n - 2);
            }
        }

        public static void PrintFibonacci()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(Fib(i));
            }

            return;

            static int Fib(int n)
            {
                if (n <= 1)
                    return n;

                return Fib(n - 1) + Fib(n - 2);
            }
        }

        public static void Factorial()
        {
            int f = 5;
            int res = 1;
            for (int i = 1; i <= f; i++)
            {
                res *= i;
            }
            Console.WriteLine(res);
        }

        public static void FactorialRecoursive()
        {
            Console.WriteLine(Factorial(5));

            return;

            static int Factorial(int n)
            {
                if (n == 1)
                    return 1;

                return n * Factorial(n - 1);
            }
        }
    }
}
