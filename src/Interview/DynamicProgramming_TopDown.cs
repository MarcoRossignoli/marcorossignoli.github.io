using System;

namespace Interview
{
    public class DynamicProgramming
    {
        // Page 134
        public static void TripleStep_TopDown()
        {

            int n = 5;
            int[] steps = new int[] { 1, 2, 3 };
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

                int lowerSolutionFound = 0;

                for (int i = 0; i < steps.Length; i++)
                {
                    lowerSolutionFound += F(n - steps[i], steps, mem);
                }

                mem[n] = lowerSolutionFound;

                return lowerSolutionFound;
            }
        }
    }
}
