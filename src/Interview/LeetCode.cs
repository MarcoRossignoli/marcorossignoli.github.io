using System;
using System.Collections.Generic;

namespace Interview
{
    class LeetCode
    {
        public static void CoinChange_TopDown()
        {
            int amount = 11;
            int[] coins = new int[] { 1, 2, 5 };
            int[] memoize = new int[amount];
            for (int i = 0; i < memoize.Length; i++)
            {
                memoize[i] = -1;
            }
            Console.WriteLine(CoinChangeMin(amount, coins, memoize));

            return;

            static int CoinChangeMin(int amount, int[] coins, int[] memoize)
            {
                if (amount == 0)
                    return 0;

                int currentSubSum = int.MaxValue;

                // Subtrac every coin
                // if we've something less than 0 there is not solution in that branch
                // when we return 0 there is a solution and we return count of edges of tree
                for (int i = 0; i < coins.Length; i++)
                {
                    int diff = amount - coins[i];

                    // if diff is negative no solution in this branch so skip and stop recursion
                    if (diff < 0)
                        continue;

                    int val = 0;
                    if (memoize[diff] != -1)
                    {
                        val = memoize[diff];
                    }
                    else
                    {
                        val = CoinChangeMin(diff, coins, memoize);
                        // we memoize result for diff = val to reuse results
                        memoize[diff] = val;
                    }

                    currentSubSum = Math.Min(val + 1, currentSubSum);
                }

                return currentSubSum;
            }

        }

        public static void CoinChange_BottomUp()
        {
            int amount = 11;
            int[] coins = new int[] { 1, 2, 5 };

            int[] problemsToSolve = new int[amount + 1];
            for (int i = 0; i < problemsToSolve.Length; i++)
            {
                problemsToSolve[i] = int.MaxValue;
            }
            problemsToSolve[0] = 0;

            for (int i = 1; i < problemsToSolve.Length; i++)
            {
                for (int k = 0; k < coins.Length; k++)
                {
                    // try a solution with coin
                    int diff = i - coins[k];
                    // if diff is minus of 0 we cannot use this coin and go on
                    if (diff < 0)
                        continue;

                    // if this coin is possible part of solution we can use it so +1
                    // and sum old solved solution
                    int calculateSolutionWithCurrentCoin = problemsToSolve[diff] + 1;

                    // if we're better than present solution override
                    int solution = Math.Min(calculateSolutionWithCurrentCoin, problemsToSolve[i]);

                    // Assign new solution
                    problemsToSolve[i] = solution;
                }
            }

            // Print solution for problem amout
            Console.WriteLine(problemsToSolve[problemsToSolve.Length - 1]);
        }

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
