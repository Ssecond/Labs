using System.Text;

namespace Practica18102023
{
    internal class ErrorLog
    {
        private const string LOG_NAME_ON_DEFAULT = "log";
        private const string DATA_TIME_FILE_FORMAT = "dd.MM.yyyy HH_mm_ss";

        private static ErrorLog? instance;
        protected ErrorLog() { }
        internal static ErrorLog Initialize()
        {
            instance ??= new ErrorLog();
            return instance;
        }
        private string GetSystemInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Операционная система (номер версии):  {Environment.OSVersion}");
            stringBuilder.AppendLine($"Разрядность процессора: {Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE")}");
            stringBuilder.AppendLine($"Модель процессора: {Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER")}");
            stringBuilder.AppendLine($"Имя пользователя: {Environment.UserName}");
            stringBuilder.AppendLine($"Число логических ядер: {Environment.ProcessorCount}");
            return stringBuilder.ToString();
        }

        private StreamWriter? streamWriter = null;
        private string logTXTName;
        internal void LogWrite(Exception exception, string fileName = LOG_NAME_ON_DEFAULT)
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                if (streamWriter == null)
                {
                    logTXTName += $"{fileName} {currentTime.ToString(DATA_TIME_FILE_FORMAT)}";
                    streamWriter = new StreamWriter(logTXTName + ".txt", false);
                    streamWriter.WriteLine("Название приложения: " + AppDomain.CurrentDomain.FriendlyName);
                    streamWriter.WriteLine("Окружение/информация ос системе.");
                    streamWriter.WriteLine(GetSystemInfo());
                }
                else
                    streamWriter = new StreamWriter(logTXTName + ".txt", true);


                
                streamWriter.WriteLine($"[{currentTime}, UTC {TimeZoneInfo.Local.GetUtcOffset(currentTime).Hours.ToString("+#;-#;0")}] {exception.Message}");
                streamWriter.WriteLine("Stack trace: " + exception.StackTrace);
            }
            catch (Exception e)
            {
                Console.WriteLine("В процессе логирования ошибки произошла непредвиденная ошибка :(");
                Console.WriteLine("Message: " + e.Message);
                Console.WriteLine("Stack trace: " + e.StackTrace);
            }
            finally
            {
                streamWriter?.Close();
            }
        }
        internal void LogWrite(string text, string fileName = LOG_NAME_ON_DEFAULT)
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                if (streamWriter == null)
                {
                    logTXTName += $"{fileName} {currentTime.ToString(DATA_TIME_FILE_FORMAT)}";
                    streamWriter = new StreamWriter(logTXTName + ".txt", false);
                    streamWriter.WriteLine("Название приложения: " + AppDomain.CurrentDomain.FriendlyName);
                    streamWriter.WriteLine("Окружение/информация ос системе.");
                    streamWriter.WriteLine(GetSystemInfo());
                }
                else
                    streamWriter = new StreamWriter(logTXTName + ".txt", true);



                streamWriter.WriteLine($"[{currentTime}, UTC {TimeZoneInfo.Local.GetUtcOffset(currentTime).Hours.ToString("+#;-#;0")}] {text}");
            }
            catch (Exception e)
            {
                Console.WriteLine("В процессе логирования ошибки произошла непредвиденная ошибка :(");
                Console.WriteLine("Message: " + e.Message);
                Console.WriteLine("Stack trace: " + e.StackTrace);
            }
            finally
            {
                streamWriter?.Close();
            }
        }
    }
}
