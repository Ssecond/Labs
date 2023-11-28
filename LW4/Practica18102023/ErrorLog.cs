using System.Text;
using System.Xml.Linq;
using System.Text.Json;

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

        private StreamWriter? streamWriter = null;
        private string logTXTName;
        internal void LogTXT(Exception exception, string fileName = LOG_NAME_ON_DEFAULT)
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                if (streamWriter == null)
                {
                    logTXTName += $"{fileName} {currentTime.ToString(DATA_TIME_FILE_FORMAT)}";
                    streamWriter = new StreamWriter(logTXTName + ".txt", false);
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
                Console.WriteLine("Stack trace: " + exception.StackTrace);
            }
            finally
            {
                streamWriter?.Close();
            }
        }

        private XDocument xmlDoc = null;
        private string logXMLName;
        internal void LogXML(Exception exception, string fileName = LOG_NAME_ON_DEFAULT)
        {
            try
            {
                DateTime currentTime = DateTime.Now;
                XElement log = new XElement("log");
                if (xmlDoc == null)
                {
                    xmlDoc = new XDocument();
                    logXMLName += $"{fileName} {currentTime.ToString(DATA_TIME_FILE_FORMAT)}";

                    XElement systemInfo = new XElement("system_info");
                    systemInfo.Add(GetSystemInfo());
                    log.Add(systemInfo);
                }

                XElement error = new XElement("error");

                    XElement time = new XElement("time");
                    time.Add(currentTime.ToString());

                    XElement timeZone = new XElement("UTC");
                    timeZone.Add(TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).Hours.ToString("+#;-#;0"));

                    XElement errorMessage = new XElement("message");
                    errorMessage.Add(exception.Message);

                error.Add(time);
                error.Add(timeZone);
                error.Add(errorMessage);

                log.Add(error);
                xmlDoc.Add(log);
                xmlDoc.Save(logXMLName + ".xml");
            }
            catch (Exception e)
            {
                Console.WriteLine("В процессе логирования ошибки произошла непредвиденная ошибка :(");
                Console.WriteLine("Message: " + e.Message);
                Console.WriteLine("Stack trace: " + exception.StackTrace);
            }
        }

        private Utf8JsonWriter jsonWriter = null;
        private MemoryStream memoryStream = null;
        private string logJSONName;
        internal void LogJSON(Exception exception, string fileName = LOG_NAME_ON_DEFAULT)
        {
            try
            {
                JsonWriterOptions options = new JsonWriterOptions
                {
                    Indented = true
                }; 
                DateTime currentTime = DateTime.Now;
                if (jsonWriter == null || memoryStream == null)
                {
                    logJSONName += $"{fileName} {currentTime.ToString(DATA_TIME_FILE_FORMAT)}";
                    memoryStream = new MemoryStream();
                    jsonWriter = new Utf8JsonWriter(memoryStream, options);
                    jsonWriter.WriteStartObject();
                    jsonWriter.WriteString("system_info", GetSystemInfo());
                }
                else
                {
                    memoryStream = new MemoryStream();
                    jsonWriter = new Utf8JsonWriter(memoryStream, options);
                    jsonWriter.WriteStartObject();
                }

                jsonWriter.WriteString("time", currentTime.ToString());
                jsonWriter.WriteString("UTC", TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).Hours.ToString("+#;-#;0"));
                jsonWriter.WriteString("message", exception.Message);
                jsonWriter.WriteEndObject();
                jsonWriter.Flush();

                string json = Encoding.UTF8.GetString(memoryStream.ToArray());
                using (StreamWriter streamWriter = new StreamWriter(logJSONName + ".json"))
                    streamWriter.Write(json);
            }
            catch (Exception e)
            {
                Console.WriteLine("В процессе логирования ошибки произошла непредвиденная ошибка :(");
                Console.WriteLine("Message: " + e.Message);
                Console.WriteLine("Stack trace: " + exception.StackTrace);
            }
        }

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
            }
            return stringBuilder.ToString();
        }
    }
}
