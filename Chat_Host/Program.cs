using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Chat_Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(Wcf_Chat.ServiceChat)))
            {
                host.Open();
                
                Console.WriteLine("Host started");
                Console.WriteLine();
                Console.ReadKey();
            }
        }

       
    }
}
