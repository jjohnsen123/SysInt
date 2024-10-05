using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BluffCityCheckIn
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MessageQueue messageQueue = null;
            if (MessageQueue.Exists(@".\Private$\AirportCheckInOutput"))
            {
                messageQueue = new MessageQueue(@".\Private$\AirportCheckInOutput");
                messageQueue.Label = "CheckIn Queue";
            }
        }
    }
}
