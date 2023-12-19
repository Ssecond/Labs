using PhoneNumbers;
using System.Net.Mail;
using System.Text;

namespace AdressBook
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Телефонная книга";
            List<AdressBookRecord> adressBook = new List<AdressBookRecord>();
            Console.WriteLine("Вас приветсвует программа «Телефонная книга»");

            Console.WriteLine("Введите, пожалуйста, команду (help — для получения списка команд).");
            string[] command;
            do
            {
                Console.Write("> ");
                command = Split(Console.ReadLine());
                switch (command[0].ToLower())
                {
                    case "help":
                        GetHelp();
                        break;
                    case "list":
                        ListOut(adressBook);
                        break;
                    case "add":
                        Add(adressBook, command);
                        break;
                    case "delete":
                        Delete(adressBook, command);
                        break;
                    case "change":
                        Change(adressBook, command);
                        break;
                    default:
                        if (command[0].ToLower() != "exit")
                            Console.WriteLine("Неверная команда. Напишите \"help\" для помощи.");
                        break;
                }
            } while (command[0] != "exit");
        }

        public static void Delete(List<AdressBookRecord> adressBook, string[] command)
        {
            try
            {
                adressBook.RemoveAt(int.Parse(command[1]) - 1);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Не то ввёл, переделывай.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Вот тут уже не понятно что произошло. Разбирайся: " + e.Message);
            }
        }

        private static void GetHelp()
        {
            using (StreamReader streamReader = new StreamReader("help.txt", Encoding.UTF8))
                Console.WriteLine(streamReader.ReadToEnd());
        }

        public static void ListOut(List<AdressBookRecord> adressBook)
        {
            if (adressBook.Count != 0)
                for (int i = 0; i < adressBook.Count; i++)
                {
                    Console.WriteLine("ID: " + (i + 1).ToString());
                    Console.WriteLine(adressBook[i].ToString());
                }
            else
                Console.WriteLine("Телефонная книга пуста.");
        }

        public static void Change(List<AdressBookRecord> adressBook, string[] command)
        {
            try
            {
                if (StartsWithUppercase(command[2]) && StartsWithUppercase(command[3]) && StartsWithUppercase(command[4]))
                {
                    PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
                    if (command.Length == 1 + 7 && phoneNumberUtil.IsValidNumber(phoneNumberUtil.Parse(command[5], "RU")))
                        adressBook[int.Parse(command[1]) - 1].ChangeTo(command[2], command[3], command[4], phoneNumberUtil.Parse(command[5], "RU"), new MailAddress(command[6]), command[7].ToLower() == "yes" ? true : false);
                    else if (command.Length == 1 + 6 && phoneNumberUtil.IsValidNumber(phoneNumberUtil.Parse(command[5], "RU")))
                        adressBook[int.Parse(command[1]) - 1].ChangeTo(command[2], command[3], command[4], phoneNumberUtil.Parse(command[5], "RU"), new MailAddress(command[6]));
                    else
                        Console.WriteLine("Неудачно, повторите попытку.");
                }
                else
                    Console.WriteLine("Неудачно, повторите попытку.");
            }
            catch (NumberParseException e)
            {
                Console.WriteLine("Неудачно, повторите попытку.");
            }
            catch (FormatException e)
            {
                Console.WriteLine("Неудачно, повторите попытку.");
            }

        }

        public static void Add(List<AdressBookRecord> adressBook, string[] command)
        {
            try
            {
                PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
                if (StartsWithUppercase(command[1]) && StartsWithUppercase(command[2]) && StartsWithUppercase(command[3]))
                {
                    if (command.Length == 1 + 6 && phoneNumberUtil.IsValidNumber(phoneNumberUtil.Parse(command[4], "RU")))
                        adressBook.Add(new AdressBookRecord(command[1], command[2], command[3], phoneNumberUtil.Parse(command[4], "RU"), new MailAddress(command[5]), command[5].ToLower() == "yes" ? true : false));
                    else if (command.Length == 1 + 5 && phoneNumberUtil.IsValidNumber(phoneNumberUtil.Parse(command[4], "RU")))
                        adressBook.Add(new AdressBookRecord(command[1], command[2], command[3], phoneNumberUtil.Parse(command[4], "RU"), new MailAddress(command[5])));
                    else
                        Console.WriteLine("Неудачно, повторите попытку.");
                }
                else
                    Console.WriteLine("Неудачно, повторите попытку.");
            }
            catch (NumberParseException e)
            {
                Console.WriteLine("Неудачно, повторите попытку.");
            }
            catch (FormatException e)
            {
                Console.WriteLine("Неудачно, повторите попытку.");
            }
        }
        public static bool StartsWithUppercase(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false; // Return false for empty strings or null
            }

            char firstChar = input[0];
            return char.IsUpper(firstChar);
        }

        private static string[] Split(string command)
        {
            List<string> commands = new List<string>();
            int i = 0;
            while (i < command.Length)
            {
                string buffer = string.Empty;
                if (command[i] != '"')
                {
                    do
                    {
                        buffer += command[i++];
                    }
                    while (i < command.Length && command[i] != ' ');
                }
                else
                {
                    i++; // to prevent '"' being added to the beginning of the command
                    do
                    {
                        if (command[i] == '\\')
                        {
                            buffer += command[++i];
                            i++;
                        }
                        else
                            buffer += command[i++];
                    }
                    while (command[i] != '"');
                    i++; // to prevent spaces from being added to the beginning of the command
                }
                commands.Add(buffer);
                i++;
            }
            return commands.ToArray();
        }
    }
}
