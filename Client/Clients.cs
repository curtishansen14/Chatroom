using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Client
{
    public class Clients
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
        public Clients(string IP, int port)
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
            UI.DisplayMessage(Encoding.ASCII.GetString(recievedMessage));
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

        private byte[] ObjectToByteArray(Clients client)
        {
            if(this == null)
            {
                return null;
            }
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, this);
            return ms.ToArray();
        }
    }
}
