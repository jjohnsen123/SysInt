using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BluffCitySplitter
{
    internal class Resequencer
    {
        private MessageQueue mqPassenger;
        private MessageQueue mqLuggage;
        private MessageQueue mqOutput;

        public Resequencer()
        {
            mqPassenger = new MessageQueue(@".\Private$\AirportCheckInPassenger");
            mqPassenger.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });

            mqLuggage = new MessageQueue(@".\Private$\AirportCheckInLuggage");
            mqLuggage.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });

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

        public void ProcessMessages()
        {
            List<XElement> messages = new List<XElement>();
            
            // Read from passenger queue
            while (mqPassenger.GetAllMessages().Length > 0)
            {
                var msg = mqPassenger.Receive();
                XElement passengerMsg = XElement.Parse(msg.Body.ToString());
                messages.Add(passengerMsg);
            }

            // Read from luggage queue
            while (mqLuggage.GetAllMessages().Length > 0)
            {
                var msg = mqLuggage.Receive();
                XElement luggageMsg = XElement.Parse(msg.Body.ToString());
                messages.Add(luggageMsg);
            }

            // Order messages by MessageId
            var orderedMessages = messages.OrderBy(m => (int) m.Element("MessageId")).ToList();

            // Send to output queue
            foreach (var message in orderedMessages)
            {
                mqOutput.Send(message.ToString());
                Console.WriteLine($"Sent to output: {message}");
            }
        }
    }
}
