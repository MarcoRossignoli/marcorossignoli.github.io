using System;

namespace Interview
{
    public class ArrayAndStrings
    {
        public static void IsUniqueBinarySearch()
        {
            Console.WriteLine(IsUnique(""));
            Console.WriteLine(IsUnique("A"));
            Console.WriteLine(IsUnique(null));
            Console.WriteLine(IsUnique("ABC"));
            Console.WriteLine(IsUnique("ABBC"));

            return;

            static bool IsUnique(string str)
            {
                if (str is null || str.Length <= 1)
                    return true;

                char[] c = str.ToCharArray();
                Array.Sort(c);
                string str2 = new string(c);
                for (int i = 0; i < str2.Length; i++)
                {
                    string before = str2.Substring(0, i);
                    string after = str2.Substring(i + 1);
                    if (!IsUniqueInternal(before + after, str2[i], 0, str2.Length - 1))
                        return false;
                }

                return true;
            }

            static bool IsUniqueInternal(string str, char c, int start, int end)
            {
                if (start >= end)
                    return true;

                int mid = (start + end) / 2;

                if (str[mid] == c)
                    return false;

                else if (str[mid] > c)
                {
                    return IsUniqueInternal(str, c, start, mid - 1);
                }
                else
                {
                    return IsUniqueInternal(str, c, mid + 1, end);
                }
            }

        }

        public static void IsUnique()
        {
            Console.WriteLine(IsUnique(""));
            Console.WriteLine(IsUnique("A"));
            Console.WriteLine(IsUnique(null));
            Console.WriteLine(IsUnique("ABC"));
            Console.WriteLine(IsUnique("ABBC"));

            return;

            static bool IsUnique(string s)
            {
                if (s is null || s.Length <= 1)
                    return true;

                for (int i = 0; i < s.Length; i++)
                {
                    for (int k = 0; k < s.Length; k++)
                    {
                        if (i == k)
                            continue;

                        if (s[i] == s[k])
                            return false;
                    }
                }

                return true;
            }

        }
    }
}
