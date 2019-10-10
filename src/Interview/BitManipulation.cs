using System;
using System.Collections.Generic;
using System.Text;

namespace Interview
{
    class BitManipulation
    {
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
}
