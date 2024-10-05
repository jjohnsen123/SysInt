using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace Opgave2
{
    public class AirTrafficControl
    {
        static void Main(string[] args)
        {
            List<string> paths = new List<string>();
            paths.Add(@".\Private$\KLM");

            foreach (string p in paths)
            {
                if (!MessageQueue.Exists(p))
                {
                    MessageQueue.Create(p);
                }
                else
                {

                }
            }

            MessageQueue klm = new MessageQueue(@".\Private$\KLM");
            sendPremadeFlightMessages(klm);
        }

        public static void sendPremadeFlightMessages(MessageQueue airline)
        {
            ETA eta1 = new ETA("KLM", "BY368", "Boeing 737-8K2", "118", "09:55:00");
            ETA eta2 = new ETA("KLM", "BY268", "Boeing 710-7BH", "118", "10:15:00");
            ETA eta3 = new ETA("KLM", "BX172", "Boeing 737-8K2", "118", "11:12:30");
            List<ETA> incomingPlanes = new List<ETA>();
            incomingPlanes.Add(eta1);
            incomingPlanes.Add(eta2);
            incomingPlanes.Add(eta3);

            foreach (ETA eta in incomingPlanes)
            {
                var jsonObj = Newtonsoft.Json.JsonConvert.SerializeObject(eta);
                // var jsonObj = eta.ToString();
                Console.WriteLine(jsonObj);
                airline.Send(jsonObj, "Label");
            }
            Console.ReadLine();
        }
    }
}
