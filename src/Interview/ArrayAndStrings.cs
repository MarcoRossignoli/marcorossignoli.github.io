using System;
using System.Collections.Generic;
using System.Text;

namespace Interview
{
    public class ArrayAndStrings
    {
        public static void RotateMatrix_Pg203()
        {
            int[][] m = new int[][]
            {
                new[] { 1, 2, 3, 4},
                new[] { 5, 6, 7, 8},
                new[] { 9, 10, 11, 12},
                new[] { 13, 14, 15, 16}
            };

            for (int i = 0; i < m.Length; i++)
            {
                for (int k = 0; k < m.Length; k++)
                {
                    Console.Write(m[i][k] + " ");
                }
                Console.WriteLine();
            }

            Rotate(m);

            Console.WriteLine();

            for (int i = 0; i < m.Length; i++)
            {
                for (int k = 0; k < m.Length; k++)
                {
                    Console.Write(m[i][k] + " ");
                }
                Console.WriteLine();
            }


            return;

            static void Rotate(int[][] m)
            {
                int n = m.Length;

                for (int layer = 0; layer < n / 2; layer++)
                {
                    int first = layer;
                    int last = n - 1 - layer;
                    for (int i = first; i < last; i++)
                    {
                        int offset = i - first;

                        // save tmp
                        int top = m[first][i];

                        // left -> top
                        m[first][i] = m[last - offset][first];
                        // bottom -> left
                        m[last - offset][first] = m[last][last - offset];
                        // right -> bottom
                        m[last][last - offset] = m[i][last];
                        // top -> right
                        m[i][last] = top;
                    }
                }

            }

        }

        public static void RotateMatrix()
        {
            int[][] m = new int[][]
            {
                new[] { 1, 2, 3, 4, 5},
                new[] { 6 ,7 ,8 , 9 , 10 },
                new[] { 11 ,12, 13, 14 ,15 },
                new[] { 16, 17, 18, 19, 20 },
                new[] { 21, 22, 23, 24, 25 }
            };

            for (int i = 0; i < m.Length; i++)
            {
                for (int k = 0; k < m.Length; k++)
                {
                    Console.Write(m[i][k] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();

            var res = Rotate(m);

            for (int i = 0; i < m.Length; i++)
            {
                for (int k = 0; k < m.Length; k++)
                {
                    Console.Write(res[i][k] + " ");
                }
                Console.WriteLine();
            }

            return;

            static int[][] Rotate(int[][] m)
            {
                int[][] m2 = new int[m.Length][];
                for (int i = 0; i < m.Length; i++)
                {
                    m2[i] = new int[m.Length];
                }

                for (int i = m.Length - 1; i >= 0; i--)
                {
                    for (int k = 0; k < m.Length; k++)
                    {
                        m2[k][i] = m[(m.Length - 1) - i][k];
                    }
                }

                return m2;
            }
        }

        public static void StringCompress()
        {
            Console.WriteLine(StringCompress("aabcccccaaa"));
            Console.WriteLine(StringCompress("a"));
            Console.WriteLine(StringCompress(""));
            Console.WriteLine(StringCompress(null));

            return;

            static string StringCompress(string str)
            {
                if (str is null || str.Length <= 1)
                    return str;

                int c = 0;
                int i = 1;
                StringBuilder final = new StringBuilder();
                final.Append(str[0]);

                while (i < str.Length)
                {
                    if (str[i] == final[final.Length - 1])
                    {
                        c++;
                    }
                    else
                    {
                        final.Append(c + 1);
                        final.Append(str[i]);
                        c = 0;
                    }
                    i++;
                }

                final.Append(c + 1);

                return str.Length <= final.Length ? str : final.ToString();
            }

        }

        public static void OneAway()
        {

            Console.WriteLine(IsOneAway("PALE", "PLE"));
            Console.WriteLine(IsOneAway("PALES", "PALE"));
            Console.WriteLine(IsOneAway("PALE", "BALE"));
            Console.WriteLine(IsOneAway("PALE", "BAKE"));
            Console.WriteLine(IsOneAway("PPALE", "PAPLE"));
            Console.WriteLine(IsOneAway("PALE", "BAKEE"));
            Console.WriteLine(IsOneAway("AB", "B"));
            Console.WriteLine(IsOneAway("AB", ""));
            Console.WriteLine(IsOneAway("A", "B"));
            Console.WriteLine(IsOneAway("A", "BC"));

            return;

            static bool IsOneAway(string a, string b)
            {
                if (Math.Abs(a.Length - b.Length) > 1)
                    return false;

                string smaller = a.Length < b.Length ? a : b;
                string bigger = smaller == a ? b : a;

                int indexBigger = 0;
                int indexSmaller = 0;
                int diff = 0;

                while (indexBigger < bigger.Length && indexSmaller < smaller.Length && diff <= 1)
                {
                    if (smaller[indexSmaller] != bigger[indexBigger])
                    {
                        diff++;
                        if (smaller.Length != bigger.Length)
                        {
                            indexBigger++;
                            continue;
                        }
                    }

                    indexBigger++;
                    indexSmaller++;
                }

                return diff <= 1;
            }

        }

        public static void Perm()
        {
            Console.WriteLine(IsPalindromePerm("Tact Coa", ""));
            // Console.WriteLine(IsPalindromePerm("BB A", ""));

            return;

            static bool IsPalindrome(string str)
            {
                int h = 0;
                int t = str.Length - 1;
                while (h < t)
                {
                    if (str[h] == ' ')
                    {
                        h++;
                        continue;
                    }

                    if (str[t] == ' ')
                    {
                        t--;
                        continue;
                    }

                    if (char.ToLower(str[h]) != char.ToLower(str[t]))
                        return false;
                    h++;
                    t--;
                }
                return true;
            }

            static bool IsPalindromePerm(string str, string prefix)
            {
                if (str.Length == 0)
                {
                    return IsPalindrome(prefix);
                }

                for (int i = 0; i < str.Length; i++)
                {
                    string before = str.Substring(0, i);
                    string after = str.Substring(i + 1);
                    if (IsPalindromePerm(before + after, prefix + str[i]))
                        return true;
                }

                return false;
            }
        }

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
