using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BluffCitySplitter
{
    internal class Aggregator
    {
        private MessageQueue mqOutput;

        public Aggregator ()
        {
            mqOutput = CreateOutputQueue();
            mqOutput.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
        }

        private MessageQueue CreateOutputQueue()
        {
            string queuePath = @".\Private$\AirportCheckInOutput";
            if (!MessageQueue.Exists(queuePath))
            {
                MessageQueue.Create(queuePath);
            }
            return new MessageQueue(queuePath);
        }

        public void AggregateMessages()
        {
            var aggregatedData = new XElement("AggregatedData");

            // Collect messages from the output queue
            while (mqOutput.GetAllMessages().Length > 0)
            {
                var msg = mqOutput.Receive();
                XElement message = XElement.Parse(msg.Body.ToString());

                aggregatedData.Add(message);
            }

            if (aggregatedData.HasElements)
            {
                mqOutput.Send(aggregatedData.ToString(), "Aggregated Message");
                Console.WriteLine($"Sent aggregated data: {aggregatedData}");
            }
            else
            {
                Console.WriteLine("No messages to aggregate.");
            }
        }
    }
}
