namespace TableCount
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] table = Calculate(5, 5, (x, y) => x * y);
            PrintTable(table);
        }
        private static void PrintTable(int[,] table, int countDigits = 2)
        {
            string numberOutFormat = $"{{0,{countDigits}}} ";
            // Print top number row
            Console.Write(new string(' ', countDigits + 1));
            for (int j = 0; j < table.GetLength(1); j++)
                Console.Write(numberOutFormat, j + 1);
            Console.WriteLine();

            for (int i = 0; i < table.GetLength(0); i++)
            {
                Console.Write(numberOutFormat, i + 1); // Print left number column
                for (int j = 0; j < table.GetLength(1); j++)
                    Console.Write(numberOutFormat, table[i, j]);
                Console.WriteLine();
            }
        }

        delegate int MathAction(int x, int y);
        private static int[,] Calculate(int x, int y, MathAction action)
        {
            int[,] table = new int[x, y];
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    table[i, j] = action(i + 1, j + 1);
            return table;
        }
    }
}