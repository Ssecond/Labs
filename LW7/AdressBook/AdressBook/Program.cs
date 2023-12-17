using System.ComponentModel.DataAnnotations;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;

namespace AdressBook
{
    internal class Program
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
                        using (StreamReader streamReader = new StreamReader("help.txt", Encoding.UTF8))
                            Console.WriteLine(streamReader.ReadToEnd());
                        break;
                    case "list":
                        if (adressBook.Count != 0)
                            for (int i = 0; i < adressBook.Count; i++)
                            {
                                Console.WriteLine("ID: " + (i + 1).ToString());
                                Console.WriteLine(adressBook[i].ToString());
                            }
                        else
                            Console.WriteLine("Телефонная книга пуста.");
                        break;
                    case "add":
                        if (command.Length == 1 + 6)
                            adressBook.Add(new AdressBookRecord(command[1], command[2], command[3], command[4], command[5], command[5].ToLower() == "да" ? true : false));
                        else if (command.Length == 1 + 5)
                            adressBook.Add(new AdressBookRecord(command[1], command[2], command[3], command[4], command[5]));
                        break;
                    case "delete":
                        adressBook.RemoveAt(int.Parse(command[1]) - 1);
                        break;
                    case "change":
                        if (command.Length == 1 + 7)
                            adressBook[int.Parse(command[1]) - 1].ChangeTo(command[2], command[3], command[4], command[5], command[6], command[7].ToLower() == "да" ? true : false);
                        else if (command.Length == 1 + 6)
                            adressBook[int.Parse(command[1]) - 1].ChangeTo(command[2], command[3], command[4], command[5], command[6]);
                        break;
                    default:
                        if (command[0].ToLower() != "exit")
                            Console.WriteLine("Неверная команда. Напишите \"help\" для помощи.");
                        break;
                }
            } while (command[0] != "exit");
        }
        static string[] Split(string command)
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
