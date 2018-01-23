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
        Dictionary<int, User> userList;
        Queue<string> Queue;
        FileLogger logger;

        public Server()
        {
            Queue = new Queue<string>();
            userList = new Dictionary<int, User>();
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9999);
            server.Start();
        }
        public void Run()
        {
            AcceptClient();
        
        }
        private void AcceptClient()
        {
            while (true)
            {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = server.AcceptTcpClient();
                NetworkStream stream = clientSocket.GetStream();
                client = new User(stream, clientSocket, logger);
                AddUsersToDictionary(client);
                Task chat = Task.Run(() =>
                {
                    ServerResponds(client);
                });
            } 
        }

        private void ServerResponds(User client)
        {
            while (true)
            {
                string message = client.Recieve();
                lock (message)
                {
                    Queue.Enqueue(message);
                }
                if (Queue.Count > 0)
                {
                   Respond(message);
                   Queue.Dequeue();
                }
                
            }

        }

        private void AddUsersToDictionary(User client)
        {
            
            userList.Add(userNumber, client);
            userNumber++;
            Console.WriteLine("added user");
        }

        private void Respond(string body)
        {
            for (int i = 1; i <= userList.Count; i++)
            {
                userList[i].Send(body);
            }
        }
    }
}
