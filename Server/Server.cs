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
        User user;
        TcpListener server;
        Dictionary<int, User> userList;
        FileLogger logger;


        public Server()
        {
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
                user = new User(stream, clientSocket, logger);
                AddUsersToDictionary(user);
                Task chat = Task.Run(() =>
                {
                    ServerResponds(user);
                });
            } 
        }

        private void ServerResponds(User user)
        {
           while (true)
           {
              string message = user.Recieve();
              Respond(message);
            }
        }

        private void AddUsersToDictionary(User user)
        {
            
            userList.Add(userNumber, user);
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
