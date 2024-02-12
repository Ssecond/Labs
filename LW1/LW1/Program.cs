using System;

namespace LW1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger();
            int[] testArray = { 5435, -43, 342, 0 };
            BubleSort(testArray);
            string buff = string.Empty;
            foreach (int i in testArray)
                buff += i + " ";
            logger.Log(buff);
        }
        static void BubleSort(int[] nums)
        {
            for (int write = 0; write < nums.Length; write++)
                for (int sort = 0; sort < nums.Length - 1; sort++)
                    if (nums[sort] > nums[sort + 1])
                        Swap(ref nums[sort + 1], ref nums[sort]);
        }
        static void Swap(ref int firstVar, ref int secondVar)
        {
            int buff = firstVar;
            firstVar = secondVar;
            secondVar = buff;
        }
        static void MergeSortImpl(int[] nums, int[] buffer, int l, int r)
        {
            if (l < r)
            {
                int m = (l + r) / 2;
                MergeSortImpl(nums, buffer, l, m);
                MergeSortImpl(nums, buffer, m + 1, r);

                int k = l;
                for (int i = l, j = m + 1; i <= m || j <= r;)
                {
                    if (j > r || (i <= m && nums[i] < nums[j]))
                    {
                        buffer[k] = nums[i];
                        ++i;
                    }
                    else
                    {
                        buffer[k] = nums[j];
                        ++j;
                    }
                    ++k;
                }
                for (int i = l; i <= r; ++i)
                    nums[i] = buffer[i];
            }
        }

        static void MergeSort(int[] nums)
        {
            if (nums.Length != 0)
            {
                int[] buffer = new int[nums.Length];
                MergeSortImpl(nums, buffer, 0, nums.Length - 1);
            }
        }
    }
}
