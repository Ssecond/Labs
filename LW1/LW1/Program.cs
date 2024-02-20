using System.Diagnostics;
using System.Text;

namespace LW1
{
    internal class Program
    {
        static Logger logger = new Logger();
        static bool stepLog = true;
        static void Main(string[] args)
        {
            int[] testArray;
            RandomFillArray(out testArray, 10_000);
            logger.Log("Изначальное состояние массива." + "\n\t" + ArrayToPrintString(testArray));
            Stopwatch timeCountner = Stopwatch.StartNew();
            logger.Log($"Сортировка начата.");
            //BubleSort(testArray);
            //MergeSort(testArray);
            ShakeSort(testArray);
            timeCountner.Stop();
            logger.Log($"Сортировка окончена. Времени затрачено: {timeCountner.ElapsedMilliseconds} milliseconds.");
            logger.Log($"Отсортированный массив.\n\t" + ArrayToPrintString(testArray));
        }
        static void RandomFillArray(out int[] nums, long length)
        { 
            nums = new int[length];
            Random random = new Random();
            for (long i = 0; i < length; i++)
                nums[i] = random.Next();
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
            for (int i = 0; i < nums.Length; i++)
                for (int j = 0; j < nums.Length - 1; j++)
                    if (nums[j] > nums[j + 1])
                    {
                        Swap(ref nums[j + 1], ref nums[j]);
                        if (stepLog)
                            logger.Log($"Сравнены: {nums[j + 1]} и {nums[j]}..." + "\n\tСостояние массива: " + ArrayToPrintString(nums));
                    }
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
                if (stepLog)
                    logger.Log("Массив поделён на два след. блока." + "\n\tI. " + ArrayToPrintString(nums, l, m) + "\n\tII. " + ArrayToPrintString(nums, m + 1, r));
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
            if (stepLog)
                logger.Log("Результат сортировки текущего блока: " + ArrayToPrintString(nums, l, r));
        }

        static void MergeSort(int[] nums)
        {
            int[] buffer = new int[nums.Length];
            MergeSortImpl(nums, buffer, 0, nums.Length - 1);
        }

        static void ShakeSort(int[] nums)
        {
            for (long i = 0; i < nums.Length / 2; i++)
            {
                bool swapHappened = false;
                // Проход слева направо
                for (long j = i; j < nums.Length - i - 1; j++)
                    if (nums[j] > nums[j + 1])
                    {
                        Swap(ref nums[j], ref nums[j + 1]);
                        swapHappened = true;
                        if (stepLog)
                            logger.Log($"Сравнены: {nums[j + 1]} и {nums[j]}..." + "\n\tСостояние массива: " + ArrayToPrintString(nums));
                    }

                // Проход справа налево
                for (long j = nums.Length - 2 - i; j > i; j--)
                    if (nums[j - 1] > nums[j])
                    {
                        Swap(ref nums[j - 1], ref nums[j]);
                        swapHappened = true;
                        if (stepLog)
                            logger.Log($"Сравнены: {nums[j + 1]} и {nums[j]}..." + "\n\tСостояние массива: " + ArrayToPrintString(nums));
                    }

                if (!swapHappened)
                    break;
            }
        }
    }
}
