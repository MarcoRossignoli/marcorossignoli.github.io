public class Solution
{
    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        int len = nums1.Length, len2 = nums2.Length, mid = (len + len2) / 2, prev = 0, last = 0;
        bool flag = (len + len2) % 2 == 0;

        if (len == 0)
        {
            if (flag)
                prev = nums2[mid - 1];
            last = nums2[mid];
        }
        else if (len2 == 0)
        {
            if (flag)
                prev = nums1[mid - 1];
            last = nums1[mid];
        }
        else if (nums1[0] >= nums2[len2 - 1])
        {
            last = mid < len2 ? nums2[mid] : nums1[mid - len2];
            if (flag)
                prev = mid - 1 < len2 ? nums2[mid - 1] : nums1[mid - 1 - len2];
        }
        else if (nums2[0] >= nums1[len - 1])
        {
            last = mid < len ? nums1[mid] : nums2[mid - len];
            if (flag)
                prev = mid - 1 < len ? nums1[mid - 1] : nums2[mid - 1 - len];
        }
        else
        {
            int i = 0, j = 0;
            while (mid-- >= 0)
            {
                if (flag)
                    prev = last;
                if (i == len)
                    last = nums2[j++];
                else if (j == len2)
                    last = nums1[i++];
                else if (nums1[i] > nums2[j])
                    last = nums2[j++];
                else
                    last = nums1[i++];
            }
        }

        if (flag)
            return (last + prev) / 2.0;

        return last;
    }
}
