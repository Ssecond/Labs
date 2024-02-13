using System.Diagnostics;
using System.Text;

namespace LW1
{
    internal class Program
    {
        static Logger logger = new Logger();
        static void Main(string[] args)
        {
            int[] testArray;
            RandomFillArray(out testArray, 10_000);
            logger.Log("Original array." + "\n\t" + ArrayToPrintString(testArray));
            BubleSort(testArray);
        }
        static void RandomFillArray(out int[] nums, long length)
        { 
            nums = new int[length];
            Random random = new Random();
            for (long i = 0; i < length; i++)
            {
                nums[i] = random.Next();
            }
        }
        static string ArrayToPrintString(int[] nums, long startIndex = 0, long endIndex = -1)
        {
            StringBuilder sb = new StringBuilder();
            if (endIndex == -1)
                endIndex = nums.LongLength - 1;

            for (long i = startIndex; i < endIndex; i++)
                sb.Append(nums[i] + ", ");
            sb.Append(nums[nums.LongLength - 1] + ".");
            return sb.ToString();
        }
        static void BubleSort(int[] nums)
        {
            Stopwatch timeCountner = Stopwatch.StartNew();
            logger.Log($"Buble sort started!");
            for (int write = 0; write < nums.Length; write++)
                for (int sort = 0; sort < nums.Length - 1; sort++)
                    if (nums[sort] > nums[sort + 1])
                    {
                        Swap(ref nums[sort + 1], ref nums[sort]);
                        logger.Log($"Compared: {nums[sort + 1]} and {nums[sort]}..." + "\n\tArray: " + ArrayToPrintString(nums));
                    }
            timeCountner.Stop();
            logger.Log($"Buble sort's done. Time spent: {timeCountner.ElapsedMilliseconds} milliseconds.");
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
                logger.Log("Array is divided by 2 blocks." + "\n\tI. " + ArrayToPrintString(nums, l, m) + "\n\tII. " + ArrayToPrintString(nums, m + 1, r));
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
            logger.Log("Result of sorting the current block: " + ArrayToPrintString(nums, l, r));
        }

        static void MergeSort(int[] nums)
        {
            Stopwatch timeCountner = Stopwatch.StartNew();
            int[] buffer = new int[nums.Length];
            MergeSortImpl(nums, buffer, 0, nums.Length - 1);
            logger.Log($"Merge sort's done. Time spent: {timeCountner.ElapsedMilliseconds} milliseconds.");
        }
    }
}
