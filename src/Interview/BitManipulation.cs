using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    // https://www3.ntu.edu.sg/home/ehchua/programming/java/DataRepresentation.html
    // https://www.electronics-tutorials.ws/binary/binary-fractions.html Converting Decimal to a Binary Fraction
    class BitManipulation
    {
        public static void IntToHex()
        {
            char[] charsets = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            int val = 15151487;
            StringBuilder sb = new StringBuilder();
            int radix = 16;
            while (val > 0)
            {
                sb.Append(charsets[val % radix]);
                val = val / radix;
            }
            Console.WriteLine(new string(sb.ToString().Reverse().ToArray()));
        }

        public static void IntToBinary()
        {
            int val = 4;
            char[] charsets = new char[] { '0', '1' };
            StringBuilder sb = new StringBuilder();
            int radix = 2;
            while (val > 0)
            {
                sb.Append(charsets[val % radix]);
                val = val / radix;
            }
            Console.WriteLine(new string(sb.ToString().Reverse().ToArray()));


        }

        public static void BinaryToString()
        {
            double r = 0.72;

            while (r > 0)
            {
                double r1 = r * 2;
                if (r1 >= 1)
                {
                    Console.WriteLine(1);
                    r = r1 - 1;
                }
                else
                {
                    Console.WriteLine(0);
                    r = r1;
                }
            }
        }

        public static void Insertion_Pg276()
        {
            int n = 0b_10000000000;
            int m = 0b_10011;
            int i = 2;
            int j = 6;

            int allOnes = ~0;
            int left = allOnes << (j + 1);
            int right = ((1 << i) - 1);
            //(1 << i).ToBitStringConsole();
            //(-1).ToBitStringConsole();
            //right.ToBitStringConsole();
            int mask = left | right;
            int n_cleared = n & mask;
            int m_shifted = m << i;
            Console.WriteLine(Convert.ToString(n_cleared | m_shifted, 2));
        }

        public static void Insertion()
        {
            int n = 0b_10000000000;
            int m = 0b_10011;

            Console.WriteLine(Convert.ToString(Insert(n, m, 2, 6), 2));

            return;

            static int Insert(int n, int m, int i, int j)
            {
                int ms = 0;
                int localN = n;

                for (int k = i; k <= (i + (j - i)); k++)
                {
                    bool bitVal = Get(m, ms++);
                    localN = Update(localN, k, bitVal);
                }

                return localN;
            }

            static int Update(int n, int i, bool bitVal)
            {
                int resetMask = ~(1 << i);
                int cleanN = n & resetMask;
                int bitValToSum = (bitVal ? 1 : 0) << i;
                return cleanN | bitValToSum;
            }

            static bool Get(int v, int i)
            {
                return ((1 << i) & v) != 0;
            }
        }
    }

    static class BitExt
    {
        public static void ToBitStringConsole(this int i)
        {
            Console.WriteLine(Convert.ToString(i, 2));
        }
    }
}
