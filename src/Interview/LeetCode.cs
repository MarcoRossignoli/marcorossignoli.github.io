using System;
using System.Collections.Generic;
using System.Text;

namespace Interview
{
    class LeetCode
    {
        public static void LongestPalidromic()
        {
            var result = LongestPalindrome("abacab");
            Console.WriteLine(result);
        }

        static string LongestPalindrome(string s)
        {
            if (s is null || s.Length == 1 || s.Length == 0)
                return s;

            string pal = "";

            for (int i = s.Length - 1; i >= 0; i--)
            {
                string str = LongestPalindrome(s, 0, i);
                if (str.Length > pal.Length)
                    pal = str;
            }

            for (int i = 0; i < s.Length - 1; i++)
            {
                string str = LongestPalindrome(s, i, s.Length - 1);
                if (str.Length > pal.Length)
                    pal = str;
            }

            return pal.Length == 0 ? s.Substring(0, 1) : pal;
        }

        static string LongestPalindrome(string s, int l, int r)
        {
            if (l >= r)
                return "";

            bool isPal = true;
            int il = l;
            int ir = r;
            while (il < ir)
            {
                if (s[il] != s[ir])
                { isPal = false; break; }
                il++;
                ir--;
            }
            int mid = (l + r) / 2;
            string ls = LongestPalindrome(s, l, mid);
            string rs = mid == l ? "" : LongestPalindrome(s, mid, r);
            string cs = isPal ? s.Substring(l, (r - l) + 1) : "";
            return ls.Length > rs.Length ? (ls.Length > cs.Length ? ls : cs) : (rs.Length > cs.Length ? rs : cs);
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
