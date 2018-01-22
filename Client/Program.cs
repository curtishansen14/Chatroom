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
  
                Client client1 = new Client("127.0.0.1", 9999, "User 1");
            while (true)
            {
                client1.Send();
                client1.Recieve();
            }
              //  Console.ReadLine();
        }
    }
}
