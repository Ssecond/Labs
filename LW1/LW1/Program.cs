using System;
using System.Diagnostics;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LW1
{
    internal class Program
    {
        static Logger logger = new Logger();
        static void Main(string[] args)
        {
            int[] testArray;
            RandomFillArray(out testArray, 1_000);
            logger.Log("Original array." + "\n\t" + ArrayToPrintString(testArray));
            BinaryTreeSort(testArray);
            logger.Log("Sorted array." + "\n\t" + ArrayToPrintString(testArray));
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
                sb.Append(string.Format("{0:n0}", nums[i]) + ", ");
            sb.Append(string.Format("{0:n0}", nums[nums.LongLength - 1]) + ".");

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

        public static void BinaryTreeSort(int[] nums)
        {
            Stopwatch timeCountner = Stopwatch.StartNew();
            TreeNode treeNode = new TreeNode(nums[0]);
            for (int i = 1; i < nums.Length; i++)
                treeNode.Insert(new TreeNode(nums[i]));

            logger.Log($"Binary tree sort's done. Time spent: {timeCountner.ElapsedMilliseconds} milliseconds.");
            nums = treeNode.Transform();
        }
    }
    public class TreeNode
    {

        public TreeNode(int data)
        {
            Data = data;
        }

        public int Data { get; set; }

        public TreeNode Left { get; set; }

        public TreeNode Right { get; set; }

        public void Insert(TreeNode node)
        {
            if (node.Data < Data)
            {
                if (Left == null)
                    Left = node;
                else
                    Left.Insert(node);
            }
            else
            {
                if (Right == null)
                    Right = node;
                else
                    Right.Insert(node);
            }
        }
        public int[] Transform(List<int>? elements = null)
        {
            if (elements == null)
                elements = new List<int>();

            if (Left != null)
                Left.Transform(elements);

            elements.Add(Data);

            if (Right != null)
                Right.Transform(elements);

            return elements.ToArray();
        }

    }
}
