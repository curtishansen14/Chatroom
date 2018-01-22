using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server
{
    public class User : IFileLogger
    {
        NetworkStream stream;
        TcpClient client;
        

        public string UserId;
        public User(NetworkStream Stream, TcpClient Client)
        {
            stream = Stream;
            client = Client;
            UserId = "495933b6-1762-47a1-b655-483510072e73";
        }
        public void Send(string Message)
        {
            byte[] message = Encoding.ASCII.GetBytes(Message);
            stream.Write(message, 0, message.Count());
            LogMessage(Message);
            Console.WriteLine("Message logged!");
        }
        public string Recieve()
        {
            byte[] recievedMessage = new byte[256];
            stream.Read(recievedMessage, 0, recievedMessage.Length);
            string recievedMessageString = Encoding.ASCII.GetString(recievedMessage);
            Console.WriteLine(recievedMessageString);
            return recievedMessageString;
        }

        public void LogMessage(string message)
        {
            StreamWriter logFile = new StreamWriter("ChatRoomLog.txt", append: true);
            logFile.WriteLine(message);
            logFile.Close();
        }

    }
}
