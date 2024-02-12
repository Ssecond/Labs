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
        internal void LogTXT(Exception exception, string fileName = LOG_NAME_ON_DEFAULT)
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
                XElement log;

                XElement error = new XElement("error");

                    XElement time = new XElement("time");
                    time.Add(currentTime.ToString());

                    XElement timeZone = new XElement("UTC");
                    timeZone.Add(TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).Hours.ToString("+#;-#;0"));

                    XElement errorMessage = new XElement("message");
                    errorMessage.Add(exception.Message);

                    XElement stackTrace = new XElement("stack_trace");
                    stackTrace.Add(exception.StackTrace);
                if (xmlDoc == null)
                {
                    log = new XElement("log");
                    xmlDoc = new XDocument();
                    logXMLName += $"{fileName} {currentTime.ToString(DATA_TIME_FILE_FORMAT)}";

                    XElement appName = new XElement("app_name");
                    appName.Add(AppDomain.CurrentDomain.FriendlyName);

                    XElement systemInfo = new XElement("system_info");
                    systemInfo.Add();

                        XElement osName = new XElement("OS");
                        osName.Add(Environment.OSVersion);

                        XElement processorName = new XElement("processor");
                        processorName.Add(Environment.OSVersion);

                        XElement processorArchitecture = new XElement("processor_architecture");
                        processorArchitecture.Add(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));

                        XElement processorIdentifier = new XElement("processor_identifier");
                        processorIdentifier.Add(Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER"));

                        XElement userName = new XElement("user_name");
                        userName.Add(Environment.UserName);

                        XElement processorCount = new XElement("processor_count");
                        processorCount.Add(Environment.ProcessorCount);

                    systemInfo.Add(osName);
                    systemInfo.Add(processorName);
                    systemInfo.Add(processorArchitecture);
                    systemInfo.Add(processorIdentifier);
                    systemInfo.Add(userName);
                    systemInfo.Add(processorCount);

                    log.Add(appName);
                    log.Add(systemInfo);

                    log.Add(error);
                    xmlDoc.Add(log);
                }
                else
                {
                    xmlDoc = XDocument.Load(logXMLName + ".xml");
                    xmlDoc.Element("log").Add(error);
                }



                error.Add(time);
                error.Add(timeZone);
                error.Add(errorMessage);
                error.Add(stackTrace);

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
                    jsonWriter.WriteString("app_name", AppDomain.CurrentDomain.FriendlyName);
                    jsonWriter.WriteString("OS", Environment.OSVersion.ToString());
                    jsonWriter.WriteString("processor", Environment.OSVersion.ToString());
                    jsonWriter.WriteString("processor_architecture", Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));
                    jsonWriter.WriteString("processor_identifier", Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER"));
                    jsonWriter.WriteString("user_name", Environment.UserName);
                    jsonWriter.WriteString("processor_count", Environment.ProcessorCount.ToString());
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
                jsonWriter.WriteString("stack_trace", exception.StackTrace);
                jsonWriter.WriteEndObject();
                jsonWriter.Flush();

                string json = Encoding.UTF8.GetString(memoryStream.ToArray());
                using (StreamWriter streamWriter = new StreamWriter(logJSONName + ".json", true))
                    streamWriter.WriteLine(json);
            }
            catch (Exception e)
            {
                Console.WriteLine("В процессе логирования ошибки произошла непредвиденная ошибка :(");
                Console.WriteLine("Message: " + e.Message);
                Console.WriteLine("Stack trace: " + exception.StackTrace);
            }
        }
    }
}
