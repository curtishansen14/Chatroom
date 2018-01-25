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
     public class Server : INotifier
    {
        private int userNumber = 1;
        User user;
        TcpListener server;
        Dictionary<int, User> userList;
        FileLogger logger;
        string userName;


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
            userName = user.Recieve();
            Notify(userName + " has entered the chatroom.");
          
            try
            {
                while (true)
                {
                    string message = user.Recieve();
                    Notify(message);
                }
            }
            catch (Exception)
            {
                logger = new FileLogger();
                logger.LogMessage(userName + " has left the chatroom");
                AcceptClient();
            }
        }

        private void AddUsersToDictionary(User user)
        {    
            userList.Add(userNumber, user);
            userNumber++;
        }

        public void Notify(string message)
        {
            for (int i = 1; i <= userList.Count; i++)
            {
                userList[i].Send(message);
            }

            logger = new FileLogger();
            logger.LogMessage(message);
        }
    }
}
