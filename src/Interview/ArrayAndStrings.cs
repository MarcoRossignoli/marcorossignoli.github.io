using System;

namespace Interview
{
    public class ArrayAndStrings
    {
        public static void URLify()
        {
            char[] str = new char[] { 'M', 'r', ' ', 'J', 'o', 'h', 'n', ' ', 'S', 'm', 'i', 't', 'h', ' ', ' ', ' ', ' ' };
            Console.WriteLine(new string(str) + " " + new string(str).Length);
            Urlfy(str, 13);
            Console.WriteLine(new string(str) + " " + new string(str).Length);
            return;

            static int CountSpace(char[] str, int len)
            {
                int s = 0;
                for (int i = 0; i < len; i++)
                {
                    if (str[i] == ' ')
                        s++;
                }
                return s;
            }

            static void Urlfy(char[] str, int len)
            {
                int p = (CountSpace(str, len) * 2 + len) - 1;
                for (int i = len - 1; i >= 0; i--)
                {
                    if (str[i] != ' ')
                    {
                        str[p--] = str[i];
                    }
                    else
                    {
                        str[p--] = '0';
                        str[p--] = '2';
                        str[p--] = '%';
                    }
                }
            }
        }

        public static void IsUniqueBookSolution_3()
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

                char[] charArray = str.ToCharArray();
                Array.Sort(charArray);
                str = new string(charArray);

                for (int i = 0; i < str.Length - 1; i++)
                {
                    if (str[i] == str[i + 1])
                        return false;
                }

                return true;
            }
        }

        public static void IsUniqueBookSolution_2()
        {
            Console.WriteLine(IsUnique(""));
            Console.WriteLine(IsUnique("A"));
            Console.WriteLine(IsUnique(null));
            Console.WriteLine(IsUnique("ABC"));
            Console.WriteLine(IsUnique("ABBC"));

            return;

            static bool IsUnique(string str)
            {
                if (str is null)
                    return true;

                if (str.Length > 128)
                    return false;

                int checker = 0;

                for (int i = 0; i < str.Length; i++)
                {
                    int val = str[i] - (int)'A';
                    if ((checker & (1 << val)) != 0)
                        return false;
                    checker |= (1 << val);
                }
                return true;
            }
        }

        public static void IsUniqueBookSolution_1()
        {
            Console.WriteLine(IsUnique(""));
            Console.WriteLine(IsUnique("A"));
            Console.WriteLine(IsUnique(null));
            Console.WriteLine(IsUnique("ABC"));
            Console.WriteLine(IsUnique("ABBC"));

            return;

            static bool IsUnique(string str)
            {
                if (str is null)
                    return true;

                if (str.Length > 128)
                    return false;

                bool[] found = new bool[128];
                for (int i = 0; i < str.Length; i++)
                {
                    int val = str[i];
                    if (found[val])
                    {
                        return false;
                    }
                    found[val] = true;
                }
                return true;
            }
        }

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
