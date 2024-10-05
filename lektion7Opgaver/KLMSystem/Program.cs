using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace KLMSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MessageQueue klm = new MessageQueue(@".\Private$\KLM");
            klm.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
            Router router = new Router(klm);
            while (true)
            {
                Console.ReadLine();
            }
            
        }
    }
}
