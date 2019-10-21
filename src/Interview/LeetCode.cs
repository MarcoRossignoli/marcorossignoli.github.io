using System;
using System.Collections.Generic;
using System.Text;

namespace Interview
{
    class LeetCode
    {
        public static void SpiralMatrix()
        {
            int[][] matrix = new int[][]
            {
                new int[]{ 1,2,3,4},
                new int[]{ 5,6,7,8},
                new int[]{ 9,10,11,12}
            };

            List<int> spiralOrder = new List<int>();

            int colBegin = 0;
            int colEnd = matrix[0].Length - 1;
            int rowBegin = 0;
            int rowEnd = matrix.Length - 1;

            while (colBegin <= colEnd && rowBegin <= rowEnd)
            {
                for (int i = colBegin; i <= colEnd; i++)
                {
                    spiralOrder.Add(matrix[rowBegin][i]);
                }

                rowBegin++;

                for (int i = rowBegin; i <= rowEnd; i++)
                {
                    spiralOrder.Add(matrix[i][colEnd]);
                }

                colEnd--;

                if (rowBegin <= rowEnd)
                {
                    for (int i = colEnd; i >= colBegin; i--)
                    {
                        spiralOrder.Add(matrix[rowEnd][i]);
                    }
                }

                rowEnd--;

                if (colBegin <= colEnd)
                {
                    for (int i = rowEnd; i >= rowBegin; i--)
                    {
                        spiralOrder.Add(matrix[i][colBegin]);
                    }
                }

                colBegin++;
            }
        }

        public static void StringToInteger()
        {
            Console.WriteLine(MyAtoi("   -42"));
            Console.WriteLine(MyAtoi("4193 with words"));
            Console.WriteLine(MyAtoi("words and 987"));
            Console.WriteLine(MyAtoi("-91283472332"));
            Console.WriteLine(MyAtoi("+-2"));
            Console.WriteLine(MyAtoi("42"));
            Console.WriteLine(MyAtoi("-6147483648"));

            return;

            int MyAtoi(string str)
            {
                int index = 0, sign = 1, total = 0;
                //1. Remove spaces
                while (index < str.Length && str[index] == ' ')
                    index++;
                //2. Get sign
                sign = index < str.Length && (str[index] == '+' || str[index] == '-') ? str[index++] == '+' ? 1 : -1 : 1;
                //3. Calculate it and take care of overflow
                while (index < str.Length)
                {
                    int digit = str[index++] - '0';
                    if (digit < 0 || 9 < digit)
                        break;
                    if (int.MaxValue / 10 < total || int.MaxValue / 10 == total && int.MaxValue % 10 < digit)
                        return sign == -1 ? int.MinValue : int.MaxValue;
                    total = total * 10 + digit;
                }
                return total * sign;
            }
        }
    }
}
