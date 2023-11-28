using System.Text;
using System.Xml;

namespace Practica18102023
{
    internal class ErrorLog
    {
        const string LOG_NAME_ON_DEFAULT = "log";
        private static ErrorLog? instance;
        protected ErrorLog() { }
        internal static ErrorLog Initialize()
        {
            instance ??= new ErrorLog();
            return instance;
        }

        private StreamWriter? streamWriter = null;
        private string logTXTName;
        internal void LogTXT(Exception exception, string fileName = LOG_NAME_ON_DEFAULT)
        {
            try
            {

                if (streamWriter == null)
                {
                    logTXTName += $"{fileName} {DateTime.Now.ToString("dd.MM.yyyy HH_mm_ss")}";
                    streamWriter = new StreamWriter(logTXTName + ".txt", false);
                    streamWriter.WriteLine(GetSystemInfo());
                }
                else
                    streamWriter = new StreamWriter(logTXTName + ".txt", true);


                streamWriter.WriteLine($"[{DateTime.Now}, UTC {TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).Hours.ToString("+#;-#;0")}] {exception.Message}");
                streamWriter.WriteLine("Stack trace: " + exception.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine("В процессе логирования ошибки произошла непредвиденная ошибка :(");
                Console.WriteLine("Message: " + e.Message);
                Console.WriteLine("Stack trace: " + exception.StackTrace);
            }
            finally
            {
                streamWriter?.Close();
            }
        }

        internal void LogXML(Exception exception, string fileName = LOG_NAME_ON_DEFAULT)
        {
            try
            {
            }
            catch (Exception e)
            {
                Console.WriteLine("В процессе логирования ошибки произошла непредвиденная ошибка :(");
                Console.WriteLine("Message: " + e.Message);
                Console.WriteLine("Stack trace: " + exception.StackTrace);
            }
            finally
            {
            }
        }
        internal void LogJSON(Exception exception, string fileName = LOG_NAME_ON_DEFAULT)
        { }

        private string GetSystemInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Операционная система (номер версии):  {Environment.OSVersion}");
            stringBuilder.AppendLine($"Разрядность процессора: {Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE")}");
            stringBuilder.AppendLine($"Модель процессора: {Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER")}");
            stringBuilder.AppendLine($"Имя пользователя: {Environment.UserName}");
            stringBuilder.AppendLine($"Число логических ядер: {Environment.ProcessorCount}");

            stringBuilder.AppendLine("Локальные диски.");
            foreach (DriveInfo dI in DriveInfo.GetDrives())
            {
                long diskSize = dI.TotalSize / 1024 / 1024 / 1024;
                long diskFreeSpace = dI.AvailableFreeSpace / 1024 / 1024 / 1024;
                stringBuilder.AppendLine($"Диск: {dI.Name}");
                stringBuilder.AppendLine($"Формат диска: {dI.DriveFormat}");
                stringBuilder.AppendLine($"Размер диска (ГБ): {diskSize}");
                stringBuilder.AppendLine($"Доступное свободное место (ГБ): {diskFreeSpace}");
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }
    }
}
