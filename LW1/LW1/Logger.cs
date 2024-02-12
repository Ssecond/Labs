namespace LW1
{
    internal class Logger
    {
        private const string LOG_NAME_ON_DEFAULT = "log";
        private const string DATA_TIME_FILE_FORMAT = "dd.MM.yyyy HH_mm_ss";

        private StreamWriter? streamWriter = null;
        private string? logTXTName;
        internal void Log(string text, string fileName = LOG_NAME_ON_DEFAULT)
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                if (streamWriter == null)
                {
                    logTXTName += $"{fileName} {currentTime.ToString(DATA_TIME_FILE_FORMAT)}";
                    streamWriter = new StreamWriter(logTXTName + ".txt", false);
                    streamWriter.WriteLine("Название приложения: " + AppDomain.CurrentDomain.FriendlyName);
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
