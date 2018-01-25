using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Client
    {
        TcpClient clientSocket;
        NetworkStream stream;
        private string userName;
        private bool connection; 


        public string GetUserName()
        {
            Console.WriteLine("Enter a name: ");
            userName = Console.ReadLine();
            return userName;
        }
        public Client(string IP, int port)
        {
            clientSocket = new TcpClient();
            clientSocket.Connect(IPAddress.Parse(IP), port);
            stream = clientSocket.GetStream();
            userName = GetUserName();
            connection = isConnected();
        }

        public string UserName
        {
            get
            {
                return userName;
            }
        }

        public void SendUserNameToServer()
        {

            string messageString = userName;
            byte[] message = Encoding.ASCII.GetBytes(messageString);
            stream.Write(message, 0, message.Count());
        }
        public void Send()
        {
          
            string messageString = userName + ": " +  UI.GetInput();
            byte[] message = Encoding.ASCII.GetBytes(messageString);
            stream.Write(message, 0, message.Count());
        }

        public void Recieve()
        {
            byte[] recievedMessage = new byte[256];
            stream.Read(recievedMessage, 0, recievedMessage.Length);
            UI.DisplayMessage(Encoding.ASCII.GetString(TrimByte(recievedMessage)));
        }

        private bool isConnected()
        {
            connection = clientSocket.Connected;
            return connection;
        }

        public bool Connection
        {
            get { return connection; }
        }

        public byte[] TrimByte(byte[] recievedMessage)
        {
            int i = recievedMessage.Length - 1;
            while (recievedMessage[i] == 0)
            {
                --i;
            }

            byte[] trimmedMessage = new byte[i + 1];
            Array.Copy(recievedMessage, trimmedMessage, i + 1);
            return trimmedMessage;
        }
    }
}
