using System;
using System.Collections.Generic;
using System.Text;

namespace Interview
{
    class BitManipulation
    {
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
