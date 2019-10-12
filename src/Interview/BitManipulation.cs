using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    // https://www3.ntu.edu.sg/home/ehchua/programming/java/DataRepresentation.html
    // https://www.electronics-tutorials.ws/binary/binary-fractions.html Converting Decimal to a Binary Fraction
    // https://graphics.stanford.edu/~seander/bithacks.html
    class BitManipulation
    {
        public static void NextNumberV2()
        {
            int i = 1 << 5;
            i.ToBitStringConsole();
            // all 1 after i and all 0 before inclusive i
            (i - 1).ToBitStringConsole();

            int v = 200;

            int leftMostOneIndex = GetLeftMostOne(v);
            int rightMostOneIndex = GeRightMostOne(v);
            int rightMostZero = GetRightMostZero(v, 0);
            int rightMostZeroFromLastFirstOne = GetRightMostZero(v, rightMostOneIndex + 1);

            int low = v;
            low = Update(low, leftMostOneIndex, false);
            low = Update(low, rightMostZero, true);

            int high = v;
            high = Update(high, rightMostOneIndex, false);
            high = Update(high, rightMostZeroFromLastFirstOne, true);


            Console.WriteLine(v.ToString() + " " + v.ToBitStringConsoleString());
            Console.WriteLine(low.ToString() + " " + low.ToBitStringConsoleString());
            Console.WriteLine(high.ToString() + " " + high.ToBitStringConsoleString());

            return;

            int Update(int n, int i, bool bitValue)
            {
                int v = bitValue ? 1 : 0;
                int re = ~(1 << i);
                n = n & re;
                n = n | (v << i);
                return n;
            }

            int GetRightMostZero(int v, int start)
            {
                for (int i = start; i <= 31; i++)
                {
                    if ((v & (1 << i)) == 0)
                        return i;
                }
                return -1;
            }

            int GeRightMostOne(int v)
            {
                for (int i = 0; i <= 31; i++)
                {
                    if ((v & (1 << i)) != 0)
                        return i;
                }
                return -1;
            }

            int GetLeftMostOne(int v)
            {
                for (int i = 31; i >= 0; i--)
                {
                    if ((v & (1 << i)) != 0)
                        return i;
                }
                return -1;
            }
        }

        public static void NextNumber()
        {

            int v = 200;

            Console.WriteLine(v.ToString() + " " + v.ToBitStringConsoleString());

            int numOfBitInV = GetNumOfBit(v);

            int tmp = v;
            while (true)
            {
                tmp++;
                if (GetNumOfBit(tmp) == numOfBitInV)
                {
                    Console.WriteLine(tmp.ToString() + " " + tmp.ToBitStringConsoleString());
                    break;
                }
            }

            tmp = v;
            while (tmp > 0)
            {
                tmp--;
                if (GetNumOfBit(tmp) == numOfBitInV)
                {
                    Console.WriteLine(tmp.ToString() + " " + tmp.ToBitStringConsoleString());
                    break;
                }
            }

            Console.WriteLine();

            static int GetNumOfBit(int n)
            {
                int c = 0;
                while (n > 0)
                {
                    if ((n & 1) != 0)
                        c++;

                    n >>= 1;
                }
                return c;
            }
        }
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
        public static string ToBitStringConsoleString(this int i)
        {
            return Convert.ToString(i, 2);
        }
        public static void ToBitStringConsole(this int i)
        {
            Console.WriteLine(Convert.ToString(i, 2));
        }
    }
}
