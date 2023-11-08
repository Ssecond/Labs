namespace TableCount
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            int Calculate(5, 5, (x, y) => x * y);
        }
        delegate int Do(int x, int y);
        static int[,] Calculate(int x, int y, Do func)
        {
            int[,] table = new int[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    table[x, y] = i * j;
                }
            }
        }
    }
}