using System;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Messaging;

namespace MYFirstMSMQ
{
    class Program
    {
        static void Main(string[] args)
        {

            MessageQueue messageQueue = null;
            if (MessageQueue.Exists(@".\Private$\AirportInfoGateNo"))
            {
                messageQueue = new MessageQueue(@".\Private$\AirportInfoGateNo");
                messageQueue.Label = "GateInfo Queue";
            }
            else
            {
                // Create the Queue
                MessageQueue.Create(@".\Private$\AirportInfoGateNo");
                messageQueue = new MessageQueue(@".\Private$\AirportInfoGateNo");
                messageQueue.Label = "GateInfo Queue";
            }

            MessageQueue messageQueueSAS = null;
            if (MessageQueue.Exists(@".\Private$\AirportCompanySAS"))
            {
                messageQueueSAS = new MessageQueue(@".\Private$\AirportCompanySAS");
                messageQueueSAS.Label = "SAS Queue";
            }
            else
            {
                // Create the Queue
                MessageQueue.Create(@".\Private$\AirportCompanySAS");
                messageQueueSAS = new MessageQueue(@".\Private$\AirportCompanySAS");
                messageQueueSAS.Label = "SAS Queue";
            }


            MessageQueue messageQueueKLM = null;
            if (MessageQueue.Exists(@".\Private$\AirportCompanyKLM"))
            {
                messageQueueKLM = new MessageQueue(@".\Private$\AirportCompanyKLM");
                messageQueueKLM.Label = "KLM Queue";
            }
            else
            {
                // Create the Queue
                MessageQueue.Create(@".\Private$\AirportCompanyKLM");
                messageQueueKLM = new MessageQueue(@".\Private$\AirportCompanyKLM");
                messageQueueKLM.Label = "KLM Queue";
            }

            MessageQueue messageQueueSW = null;
            if (MessageQueue.Exists(@".\Private$\AirportCompanySW"))
            {
                messageQueueSW = new MessageQueue(@".\Private$\AirportCompanySW");
                messageQueueSW.Label = "South West Queue";
            }
            else
            {
                // Create the Queue
                MessageQueue.Create(@".\Private$\AirportCompanySW");
                messageQueueSW = new MessageQueue(@".\Private$\AirportCompanySW");
                messageQueueSW.Label = "South West Queue";
            }

            AirlineCompanyPublisher airlineCompanyPublisher = null;

            airlineCompanyPublisher = new AirlineCompanyPublisher(messageQueue, messageQueueSAS, messageQueueKLM, messageQueueSW);
            while (true) { } 
        }
    }
}

