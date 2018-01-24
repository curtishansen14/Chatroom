using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
  
        static void Main(string[] args)
        {
            Client client = new Client("192.168.0.135", 9999);
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
