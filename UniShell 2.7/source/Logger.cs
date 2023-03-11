using static System.Net.Mime.MediaTypeNames;

namespace Logs
{
    public class Logger
    {
        string log;
        public string fileToLog;
        public Logger() { log = String.Empty; fileToLog = $"C:\\Users\\{Environment.UserName}\\Documents\\log.{DateTime.Now.Day}.{DateTime.Now.Hour}.{DateTime.Now.Minute}.txt"; }
        public void Log(string message)
        {
            log += $"{message}\n";
        }
        public void WriteToFile() { File.WriteAllText(fileToLog, log); }
    }
}
