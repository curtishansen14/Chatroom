using System;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
  
        static void Main(string[] args)
        {
            Client client = new Client("127.0.0.1", 9999);
            client.SendUserNameToServer();
            Parallel.Invoke(() =>
            {
                while (client.Connection == true)
                {
                    client.Send();
                }
                Console.WriteLine("Lost connection");
               
            },
           () =>
           {
               while (client.Connection == true)
               {
                   client.Recieve();
               }
               Console.WriteLine("Lost connection");
           });
        }
    }
}
