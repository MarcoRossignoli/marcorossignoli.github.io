using System;

namespace Interview
{
    public static class RandomExercise
    {
        public static void PrintPattern()
        {
            int n = 16;

            PrintPattern(n, n, true);

            n = 2;

            PrintPattern(n, n, true);

            n = 10;

            PrintPattern(n, n, true);

            PrintPatternBest(n);

            return;

            static void PrintPatternBest(int n)
            {

                // Base case (When n becomes 0 or
                // negative) 
                if (n == 0 || n < 0)
                {

                    Console.Write(n + " ");

                    return;
                }

                // First print decreasing order 
                Console.Write(n + " ");

                PrintPatternBest(n - 5);

                // Then print increasing order 
                Console.Write(n + " ");
            }

            static void PrintPattern(int start, int current, bool subtract)
            {
                if (current > start)
                {
                    Console.WriteLine("----");
                    return;
                }

                Console.WriteLine(current);

                if (current <= 0)
                {
                    PrintPattern(start, current + 5, false);
                }
                else
                {
                    PrintPattern(start, current + (subtract ? -5 : 5), subtract);
                }
            }
        }

        public static void TrappingRainWater()
        {
            int[] arr = new int[] { 3, 0, 0, 4, 0, 2 };

            int result = findWater(arr.Length, arr);

            static int findWater(int n, int[] arr)
            {
                // left[i] contains height of tallest bar to the 
                // left of i'th bar including itself 
                int[] left = new int[n];

                // Right [i] contains height of tallest bar to 
                // the right of ith bar including itself 
                int[] right = new int[n];

                // Initialize result 
                int water = 0;

                // Fill left array 
                left[0] = arr[0];
                for (int i = 1; i < n; i++)
                    left[i] = Math.Max(left[i - 1], arr[i]);

                // Fill right array 
                right[n - 1] = arr[n - 1];
                for (int i = n - 2; i >= 0; i--)
                    right[i] = Math.Max(right[i + 1], arr[i]);

                // Calculate the accumulated water element by element 
                // consider the amount of water on i'th bar, the 
                // amount of water accumulated on this particular 
                // bar will be equal to min(left[i], right[i]) - arr[i] . 
                for (int i = 0; i < n; i++)
                    water += Math.Min(left[i], right[i]) - arr[i];

                return water;
            }

        }

        public static void PrintMatrix()
        {
            int n = 3;
            int m = 3;
            int values = 0;
            int[,] matrix = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < m; k++)
                {
                    matrix[i, k] = values++;
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < m; k++)
                {
                    Console.Write(matrix[i, k]);
                }
                Console.WriteLine();
            }

            Console.WriteLine();

            int[] currentPath = new int[n + m - 1];

            printMatrix(matrix, n, m, 0, 0, currentPath, 0);

            return;

            static void printMatrix(int[,] mat, int m, int n, int i, int j, int[] path, int idx)
            {
                path[idx] = mat[i, j];

                // Reached the bottom of the matrix so we are left with 
                // only option to move right 
                if (i == m - 1)
                {
                    for (int k = j + 1; k < n; k++)
                    {
                        path[idx + k - j] = mat[i, k];
                    }
                    for (int l = 0; l < idx + n - j; l++)
                    {
                        Console.Write(path[l] + " ");
                    }
                    Console.WriteLine();
                    return;
                }

                // Reached the right corner of the matrix we are left with 
                // only the downward movement. 
                if (j == n - 1)
                {
                    for (int k = i + 1; k < m; k++)
                    {
                        path[idx + k - i] = mat[k, j];
                    }
                    for (int l = 0; l < idx + m - i; l++)
                    {
                        Console.Write(path[l] + " ");
                    }
                    Console.WriteLine();
                    return;
                }

                // Print all the paths that are possible after moving down 
                printMatrix(mat, m, n, i + 1, j, path, idx + 1);

                // Print all the paths that are possible after moving right 
                printMatrix(mat, m, n, i, j + 1, path, idx + 1);
            }
        }

        public static void CountMatrixPath()
        {
            int n = 3;
            int m = 3;
            int values = 0;
            int[,] matrix = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < m; k++)
                {
                    matrix[i, k] = values++;
                }
            }

            Console.WriteLine(Count(matrix, n, m));

            return;

            static int Count(int[,] matrix, int n, int m)
            {

                if (n == 1 || m == 1)
                {
                    return 1;
                }

                return Count(matrix, n - 1, m) + Count(matrix, n, m - 1);
            }

        }
    }
}
