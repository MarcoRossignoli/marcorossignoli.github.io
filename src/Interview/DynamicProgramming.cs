using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Interview
{
    public class DynamicProgramming
    {
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
            // TODO
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
