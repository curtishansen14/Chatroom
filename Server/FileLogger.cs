using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
