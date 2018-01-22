using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
     class Server
    {
        public static User client;
        private int userNumber;
        TcpListener server;
        Dictionary<User, int> userList;

        public Server()
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9999);
            server.Start();
        }
        public void Run()
        {
            AcceptClient();
            while (true)
            {
                string message = client.Recieve();
                Respond(message);

            }
        }
        private void AcceptClient()
        {
                userNumber = 1;
                TcpClient clientSocket = default(TcpClient);
                clientSocket = server.AcceptTcpClient();
                NetworkStream stream = clientSocket.GetStream();
                client = new User(stream, clientSocket);
                userList = new Dictionary<User, int>();
                userList.Add(client, userNumber);
                userNumber++;
        }

        private void Respond(string body)
        {
             client.Send(body);
        }
    }
}
