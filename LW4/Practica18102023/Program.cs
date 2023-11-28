using Practica18102023.Exceptions;

namespace Practica18102023
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Решение квадратного уровнения";
            Console.WriteLine("Вас приветствует программа, решающая квадратное уравнение.");
            Console.WriteLine("Важно, коофиценты считываются из файла.");
            try
            {
                int[] сoefficients = new int[3];

                StreamReader reader = new StreamReader("Datad.txt");
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
                ErrorInfoOut(e);
                // ErrorInfoOut(e);
            }
            catch (DiscriminantZero e)
            {
                ErrorInfoOut(e);
            }
            catch (DiscriminantLessZeroException e)
            {
                ErrorInfoOut(e);
            }
            catch (NotANumberException e)
            {
                ErrorInfoOut(e);
            }
            catch (Exception e)
            {
                ErrorInfoOut(e);
            }
        }
        private static void ErrorInfoOut(Exception e, string errorOccuredMessage = "\nПроизошла ошибка.")
        {
            Console.WriteLine(errorOccuredMessage);
            Console.WriteLine(e);

            ErrorLog errorLog = ErrorLog.Initialize();
            //errorLog.LogTXT(e);
            //errorLog.LogXML(e);
            errorLog.LogJSON(e);
        }
    }
}