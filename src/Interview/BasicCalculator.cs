using System;
using System.Diagnostics;

namespace Interview.Games
{
    public class BasicCalculator
    {
        public static void Calculator()
        {
            Debug.Assert(new Solution().Calculate("( (10 + 2) - 5 ) + 2") == 9);
            Debug.Assert(new Solution().Calculate(" 2 - 1 + 2 ") == 3);
            Debug.Assert(new Solution().Calculate("(1+(4+5+2)-3)+(6+8)") == 23);
        }
    }

    public class Solution
    {
        public int Calculate(string s)
        {
            int start = 0;
            return Calculate(s, ref start, s.Length - 1);
        }

        int Calculate(string expression, ref int current, int end)
        {
            int result = 0;
            bool? plus = null;
            while (current <= end)
            {
                if (expression[current] == '(')
                {
                    current++;
                    int subExpressionResult = Calculate(expression, ref current, end);
                    result = plus is null ? subExpressionResult : (plus.Value ? result + subExpressionResult : result - subExpressionResult);
                }
                else if (expression[current] == ')')
                {
                    current++;
                    break;
                }
                else if (char.IsNumber(expression[current]))
                {
                    int value = ParseInt(expression, ref current);
                    result = plus is null ? value : (plus.Value ? result + value : result - value);
                }
                else if (expression[current] == '+' || expression[current] == '-')
                {
                    plus = expression[current] == '+';
                    current++;
                }
                else if (expression[current] == ' ')
                {
                    current++;
                }
                else
                {
                    throw new InvalidOperationException("Unexpected char");
                }
            }

            return result;
        }

        int ParseInt(string str, ref int current)
        {
            int start = current;
            int end = current;
            while (end < str.Length && char.IsDigit(str[end]))
            {
                end++;
            }
            int digitCount = end - start;
            current += digitCount;
            return int.Parse(str.AsSpan().Slice(start, digitCount));
        }
    }
}
