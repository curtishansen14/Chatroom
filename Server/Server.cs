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
        private int userNumber = 1;
        User client;
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
            AddUsersToDictionary();
            while (true)
            {
                string message = client.Recieve();
                Respond(message);

            }
        }
        private void AcceptClient()
        {
               
            TcpClient clientSocket = default(TcpClient);
            clientSocket = server.AcceptTcpClient();
            NetworkStream stream = clientSocket.GetStream();
            client = new User(stream, clientSocket);  
        }

        private void AddUsersToDictionary()
        {
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
