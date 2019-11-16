using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Interview
{
    public class DynamicProgramming
    {
        public static void WildCardMatching()
        {
            bool v = IsMatch("ciao","ci*");

            return;

            static bool IsMatch(string s, string p)
            {
                if (p.Replace("*", "").Length > s.Length)
                    return false;
                bool[] d = new bool[s.Length + 1];
                d[0] = true;
                for (int i = 1; i <= p.Length; ++i)
                {
                    char pchar = p[i - 1];
                    if (pchar == '*')
                        for (int j = 1; j <= s.Length; ++j)
                            d[j] = d[j - 1] || d[j];
                    else
                        for (int j = s.Length; j >= 1; --j)
                            d[j] = d[j - 1] && (pchar == '?' || pchar == s[j - 1]);
                    d[0] = d[0] && pchar == '*';
                }
                return d[s.Length];
            }
        }
        public static void BooleanEvaluation()
        {
            string expression = "0&0|1";
            Console.WriteLine(CountEval(expression, false));
            // Console.WriteLine(CountEval(expression, true));

            return;

            static int CountEval(string s, bool result)
            {
                if (s.Length == 0)
                    return 0;
                if (s.Length == 1)
                {
                    return stringToBool(s) == result ? 1 : 0;
                }

                int ways = 0;

                for (int i = 1; i < s.Length; i += 2)
                {
                    char c = s[i];
                    string left = s.Substring(0, i);
                    string right = s.Substring(i + 1);

                    Console.WriteLine("Left " + left + " Right " + right + " op " + c + " i " + i);

                    int leftTrue = CountEval(left, true);
                    int leftFalse = CountEval(left, false);
                    int rightTrue = CountEval(right, true);
                    int rightFalse = CountEval(right, false);

                    int total = (leftTrue + leftFalse) * (rightTrue + rightFalse);

                    int totalTrue = 0;
                    if (c == '^')
                    {
                        totalTrue = leftTrue * rightFalse + leftFalse * rightTrue;
                    }
                    else if (c == '&')
                    {
                        totalTrue = leftTrue * rightTrue;
                    }
                    else if (c == '|')
                    {
                        totalTrue = leftTrue * rightTrue + leftFalse * rightTrue + leftTrue * rightFalse;
                    }

                    int subways = result ? totalTrue : total - totalTrue;
                    ways += subways;
                }

                return ways;
            }

            static bool stringToBool(string c)
            {
                return c.Equals("1");
            }
        }

        public static void Permutation_NoDup_355_3()
        {
            List<string> result = new List<string>();
            getPerms("", "ABC", result);

            foreach (var v in result)
            {
                Console.WriteLine(v);
            }

            return;

            static void getPerms(string prefix, string remainder, List<string> result)
            {
                if (remainder.Length == 0)
                {
                    result.Add(prefix);
                }

                int len = remainder.Length;
                for (int i = 0; i < len; i++)
                {
                    string before = remainder.Substring(0, i);
                    string after = remainder.Substring(i + 1);
                    char c = remainder[i];
                    getPerms(prefix + c, before + after, result);
                }
            }
        }

        public static void Permutation_NoDup_355_2()
        {

            foreach (var item in Perm("ABC"))
            {
                Console.WriteLine(item);
            }

            return;

            static List<string> Perm(string remainder)
            {
                int len = remainder.Length;

                List<string> result = new List<string>();

                if (len == 0)
                {
                    result.Add("");
                    return result;
                }

                for (int i = 0; i < len; i++)
                {
                    string before = remainder.Substring(0, i);
                    string after = remainder.Substring(i + 1);
                    List<string> partials = Perm(before + after);

                    foreach (var s in partials)
                    {
                        result.Add(remainder[i] + s);
                    }
                }

                return result;
            }

        }

        public static void Permutation_NoDup_355_1()
        {

            foreach (var item in Perm("ABC"))
            {
                Console.WriteLine(item);
            }

            return;

            static List<string> Perm(string str)
            {
                if (str is null)
                    return null;

                List<string> permutations = new List<string>();

                if (str.Length == 0)
                {
                    permutations.Add(""); // base case
                    return permutations;
                }

                char first = str[0];
                string remain = str.Substring(1);

                List<string> words = Perm(remain);

                foreach (var word in words)
                {
                    for (int i = 0; i <= word.Length; i++)
                    {
                        // Insert at
                        string start = word.Substring(0, i);
                        string end = word.Substring(i);
                        permutations.Add(start + first + end);
                    }
                }

                return permutations;
            }

        }

        public static void TowerOfHanoi()
        {
            TowerOfHanoi origin = new TowerOfHanoi(0, "Origin");
            TowerOfHanoi buffer = new TowerOfHanoi(1, "Buffer");
            TowerOfHanoi destination = new TowerOfHanoi(2, "Destination");


            int totalDisk = 3;
            for (int i = totalDisk; i > 0; i--)
            {
                origin.Add(i);
            }

            origin.MoveDisks(totalDisk, destination, buffer);
        }

        public static void PowerSet_BottomUp_Page349()
        {
            List<int> arr = new List<int>
            {
                1,
                2,
                3
            };

            var res = getSubsets(arr, 0);

            return;

            static List<List<int>> getSubsets(List<int> set, int index)
            {
                List<List<int>> allsubsets;
                if (set.Count == index)
                {
                    allsubsets = new List<List<int>>();
                    allsubsets.Add(new List<int>()); // empty set
                }
                else
                {
                    allsubsets = getSubsets(set, index + 1);
                    int item = set[index];
                    foreach (List<int> subset in allsubsets.ToArray())
                    {
                        List<int> newsubset = new List<int>();
                        newsubset.AddRange(subset);
                        newsubset.Add(item);
                        allsubsets.Add(newsubset);
                    }
                }
                return allsubsets;
            }

        }

        public static void PowerSet_TopDown()
        {
            int[] set = new int[] { 1, 2 };
            List<int[]> sets = new List<int[]>
            {
                null
            };
            for (int i = 0; i < set.Length; i++)
            {
                sets.Add(new int[] { set[i] });
            }

            Fun(set, sets);

            foreach (var fs in sets)
            {
                if (fs is null)
                {
                    Console.WriteLine("Null");
                    continue;
                }

                foreach (var v in fs)
                {
                    Console.Write(v + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine($"Total: {sets.Count}");

            return;

            static void Fun(int[] set, List<int[]> sets)
            {

                if (set.Length == 1)
                    return;

                sets.Add(set);

                if (set.Length - 1 == 1)
                    return;

                for (int i = 0; i < set.Length; i++)
                {
                    int[] subset = new int[set.Length - 1];

                    int nextIndex = 0;
                    for (int k = 0; k < set.Length; k++)
                    {
                        if (k == i)
                            continue;

                        subset[nextIndex++] = set[k];
                    }

                    Fun(subset, sets);
                }

            }

        }

        // Page 134
        public static void TripleStep_TopDown()
        {

            int n = 5;
            int[] steps = new int[] { 2, 3 };
            int[] mem = new int[n + 1];
            Console.WriteLine(F(n, steps, mem));

            return;

            static int F(int n, int[] steps, int[] mem)
            {
                if (n < 0)
                    return 0;
                if (n == 0)
                    return 1;

                if (mem[n] != 0)
                {
                    return mem[n];
                }

                for (int i = 0; i < steps.Length; i++)
                {
                    mem[n] += F(n - steps[i], steps, mem);
                }

                return mem[n];
            }
        }

        public static void TripleStep_BottomUp()
        {
            int n = 4;
            int[] mem = new int[n + 1];

            Console.WriteLine(F(n, 3));

            return;

            static int F(int n, int n1)
            {
                int[] problems = new int[n + 1];
                problems[0] = 1;
                problems[1] = 1;
                problems[2] = 2;

                for (int i = 3; i < problems.Length; i++)
                {
                    for (int k = 1; k <= n1; k++)
                    {
                        problems[i] += problems[i - k];
                    }
                }

                return problems[^1];
            }
        }

        public static void RobotInAGrid()
        {
            int[][] grid = new int[][]
            {
                        // C
                new int[] {0,0,0,0}, // R
                new int[] {0,0,1,0},
                new int[] {0,1,0,0},
                new int[] {0,0,0,0}
            };

            Console.WriteLine(Routes(grid, 0, 0));

            return;

            static int Routes(int[][] grid, int r, int c)
            {
                if (grid[r][c] == 1)
                {
                    return 0;
                }

                if (r == grid[0].Length - 1)
                {
                    for (int i = 1; i < grid.Length - 1; i++)
                    {
                        if (grid[r][i] == 1)
                            return 0;
                    }
                    return 1;
                }

                if (c == grid.Length - 1)
                {
                    for (int i = 1; i < grid[0].Length - 1; i++)
                    {
                        if (grid[i][c] == 1)
                            return 0;
                    }
                    return 1;
                }

                return Routes(grid, r + 1, c) + Routes(grid, r, c + 1);
            }

        }
    }

    [DebuggerDisplay("Name={_name}")]
    public class TowerOfHanoi
    {
        System.Collections.Generic.Stack<int> _disks;
        int _index;
        string _name;
        public TowerOfHanoi(int i, string name)
        {
            _disks = new System.Collections.Generic.Stack<int>();
            _name = name;
            _index = i;
        }

        public int Index()
        {
            return _index;
        }

        public void Add(int d)
        {
            if (_disks.Count != 0 && _disks.Peek() <= d)
            {
                throw new Exception();
            }
            else
            {
                _disks.Push(d);
            }
        }

        public void MoveTopTo(TowerOfHanoi t)
        {
            int top = _disks.Pop();
            t.Add(top);
        }

        public void MoveDisks(int n, TowerOfHanoi destination, TowerOfHanoi buffer)
        {
            if (n > 0)
            {
                MoveDisks(n - 1, buffer, destination);
                MoveTopTo(destination);
                buffer.MoveDisks(n - 1, destination, this);
            }
        }
    }
}
