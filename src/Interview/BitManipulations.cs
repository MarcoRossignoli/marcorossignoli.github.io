using System;
using System.Collections.Generic;
using System.Text;

namespace Interview
{
    public class BitManipulations
    {
        public static void Test()
        {
            // Console.WriteLine(4 >> 1);

            Console.WriteLine(GetBit(0, 0)); // set lsb


            static int GetBit(int num, int i)
            {
                return (num | (1 << i));
            }

            Console.WriteLine(ClearBit(2, 1)); // clear 00000010 -> 00000000
            static int ClearBit(int num, int i)
            {
                int mask = ~(1 << i); // i = 1 00000010 -> 11111101
                return num & mask;
            }

            Console.WriteLine(Clearmsbtoi(7, 2)); // clear 00000111 -> 00000011
            static int Clearmsbtoi(int num, int i)
            {
                int mask = (1 << i) - 1;
                return num & mask;
            }

            Console.WriteLine(Clearito0(7, 1)); // clear 00000111 -> 00000100
            static int Clearito0(int num, int i)
            {
                int mask = -1 << (i + 1);
                return num & mask;
            }

            Console.WriteLine(UpdateBit(0, 2, true)); // 00000000 -> 00000100
            static int UpdateBit(int num, int i, bool up)
            {
                int val = up ? 1 : 0; // chose between 0000000|1 and 0000000|0
                int mask = ~(1 << i); // clear selected bit i.e. 2 11111011
                return (num & mask) | (val << i); // clear selected bit and sum selected value in right position
            }
        }
    }
}
