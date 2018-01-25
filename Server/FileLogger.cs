using System.IO;

namespace Server
{
    public class FileLogger : ILogger
    {
        public void LogMessage(string message)
        {
            StreamWriter logFile = new StreamWriter("ChatRoomLog.txt", append: true);
            logFile.WriteLine(message);
            logFile.Close();
        }
    }
}
