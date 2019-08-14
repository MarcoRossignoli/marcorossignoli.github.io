using System;
using System.Collections.Generic;

namespace Interview
{
    public class DynamicProgramming
    {
        public static void PowerSet()
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
}
