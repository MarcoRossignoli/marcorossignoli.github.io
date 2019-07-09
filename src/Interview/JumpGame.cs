using System;
using System.Diagnostics;

namespace Interview.Games
{
    class JumpGame
    {
        public static void Play()
        {
            Debug.Assert(new Solution().Jump(new int[] { 2, 3, 1, 1, 4 }) == 2);
            Debug.Assert(new Solution().Jump(new int[1] { 0 }) == 0);
            Debug.Assert(new Solution().Jump(new int[2] { 2, 1 }) == 1);
            Debug.Assert(new Solution().Jump(new int[] { 5, 6, 4, 4, 6, 9, 4, 4, 7, 4, 4, 8, 2, 6, 8, 1, 5, 9, 6, 5, 2, 7, 9, 7, 9, 6, 9, 4, 1, 6, 8, 8, 4, 4, 2, 0, 3, 8, 5 }) == 5);
        }
    }

    public partial class Solution
    {
        public int Jump(int[] nums)
        {
            if (nums.Length < 2)
            {
                return 0;
            }

            int level = 0, currentMax = 0;
            int i = 0, nextMax = 0;
            // nodes count of current level > 0
            while (currentMax - i + 1 > 0)
            {
                level++;
                // traverse current level , and update the max reach of next level
                for (; i <= currentMax; i++)
                {
                    nextMax = Math.Max(nextMax, nums[i] + i);
                    // if last element is in level + 1,  then the min jump = level 
                    if (nextMax >= nums.Length - 1)
                    {
                        return level;
                    }
                }
                currentMax = nextMax;
            }

            return 0;
        }
    }
}
