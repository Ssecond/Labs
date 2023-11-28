using Practica18102023.Exceptions;

namespace Practica18102023
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string ERROR_OCCURED = "\nПроизошла ошибка.";
            /*
                Задание на лабораторную работу.

                Решение oбычного квадратного уровнения.
                1) Посчитать дискриминант: > 0; +
                2) a, b, c читаются с файла; +
                3) Написать свою кастомную ошибку, если дискременант < 0; +
                4) Обработать ошибку наличия файла; +
                5) При парсенге данных с файла попадается текст вместо числа, кастомная ошибка; +
                6) Если D = 0 => кастомная ошибка, но в текст пишем, что ур-е имеет один корень, без решения его. +
                7) Ппишем выполнилась ли программа: успешно, не успешно. +
            */
            Console.Title = "Решение квадратного уровнения";
            Console.WriteLine("Вас приветствует программа, решающая квадратное уравнение.");
            Console.WriteLine("Важно, коофиценты считываются из файла.");
            try
            {
                int[] сoefficients = new int[3];

                StreamReader reader = new StreamReader("Data.txt");
                char[] coefCh = { 'a', 'b', 'c' };
                byte i = 0;
                Console.WriteLine("В текстовом файле были найдены следующие коофиценты.");
                while (!reader.EndOfStream)
                {
                    string сoef = reader.ReadLine() ?? throw new NotANumberException("Найдено нечисловое значение.");
                    Console.WriteLine($"{coefCh[i]} = {сoef}");
                    if (!int.TryParse(сoef, out сoefficients[i]))
                        throw new NotANumberException("Найдено нечисловое значение.");
                    i++;
                }
                reader.Close();

                double discriminant = (сoefficients[1] * сoefficients[1]) - 4 * сoefficients[0] * сoefficients[2];

                if (discriminant > 0)
                {
                    Console.WriteLine("\nОтветы.");
                    Console.WriteLine("x1 = " + (-сoefficients[1] + Math.Sqrt(discriminant)) / (2 * сoefficients[0]));
                    Console.WriteLine("x2 = " + (-сoefficients[1] - Math.Sqrt(discriminant)) / (2 * сoefficients[0]));
                }
                else if (discriminant == 0)
                    throw new DiscriminantZero("Дискрименант равен нулю.");
                else
                    throw new DiscriminantLessZeroException("Дискрименант меньше нуля.");

                Console.WriteLine("Программа успешно завершила выполнение.");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(ERROR_OCCURED);
                Console.WriteLine(e);
            }
            catch (DiscriminantZero e)
            {
                Console.WriteLine(ERROR_OCCURED);
                Console.WriteLine(e);
            }
            catch (DiscriminantLessZeroException e)
            {
                Console.WriteLine(ERROR_OCCURED);
                Console.WriteLine(e);
            }
            catch (NotANumberException e)
            {
                Console.WriteLine(ERROR_OCCURED);
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(ERROR_OCCURED);
                Console.WriteLine(e);
            }
        }
    }
}