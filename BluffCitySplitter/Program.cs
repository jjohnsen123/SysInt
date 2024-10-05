using MessageQueueUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BluffCitySplitter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MessageQueue mqPassenger = null;
            if (MessageQueue.Exists(@".\Private$\AirportCheckInPassenger"))
            {
                mqPassenger = new MessageQueue(@".\Private$\AirportCheckInPassenger");
                mqPassenger.Label = "CheckIn Passenger Queue";
            }
            else
            {
                MessageQueue.Create(@".\Private$\AirportCheckInPassenger");
                mqPassenger = new MessageQueue(@".\Private$\AirportCheckInPassenger");
                mqPassenger.Label = "Newly Created Label";
            }

            MessageQueue mqLuggage = null;
            if (MessageQueue.Exists(@".\Private$\AirportCheckInLuggage"))
            {
                mqLuggage = new MessageQueue(@".\Private$\AirportCheckInLuggage");
                mqLuggage.Label = "CheckIn Passenger Queue";
            }
            else
            {
                MessageQueue.Create(@".\Private$\AirportCheckInLuggage");
                mqLuggage = new MessageQueue(@".\Private$\AirportCheckInLuggage");
                mqLuggage.Label = "Newly Created Label";
            }

            // Empties queues
            string qpPassenger = @".\Private$\AirportCheckInPassenger";
            string qpLuggage = @".\Private$\AirportCheckInLuggage";
            string qpOutput = @".\Private$\AirportCheckInOutput";
            MessageQueueClearer clearer = new MessageQueueClearer();
            clearer.ClearQueue(qpPassenger);
            clearer.ClearQueue(qpLuggage);
            clearer.ClearQueue(qpOutput);


            // Load the original XML file
            XElement checkInFile = XElement.Load(@"CheckedInPassenger.xml");
            string flightNumber = checkInFile.Element("Flight")?.Attribute("number")?.Value;
            string flightDate = checkInFile.Element("Flight")?.Attribute("Flightdate")?.Value;

            // Initialize message count for ordering
            int totalMessages = 1 + checkInFile.Elements("Luggage").Count();
            int messageId = 1;

            // Set formatter
            mqPassenger.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
            mqLuggage.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });

            //Splitting
            Console.WriteLine("\nSplitting...\n");

            // Extract and send Passenger details
            XElement passengerDetails = checkInFile.Element("Passenger");
            if (passengerDetails != null)
            {
                XElement passengerMessage = new XElement("PassengerMessage",
                    new XElement("ReservationNumber", passengerDetails.Element("ReservationNumber")?.Value),
                    new XElement("FirstName", passengerDetails.Element("FirstName")?.Value),
                    new XElement("LastName", passengerDetails.Element("LastName")?.Value),
                    new XElement("FlightNumber", flightNumber),
                    new XElement("FlightDate", flightDate),
                    new XElement("MessageId", messageId++),
                    new XElement("TotalMessages", totalMessages)
                    );
                Console.WriteLine(passengerMessage);
                mqPassenger.Send(passengerMessage.ToString(), "Passenger Information");
            }

            // Extract and send Luggage Details
            var luggageDetails = checkInFile.Elements("Luggage");
            foreach (var luggage in luggageDetails)
            {
                XElement luggageMessage = new XElement("LuggageMessage",
                    new XElement("FlightNumber", flightNumber),
                    new XElement("FlightDate", flightDate),
                    new XElement("Id", luggage.Element("Id")?.Value),
                    new XElement("Identification", luggage.Element("Identification")?.Value),
                    new XElement("Category", luggage.Element("Category")?.Value),
                    new XElement("Weight", luggage.Element("Weight")?.Value),
                    new XElement("MessageId", messageId++),
                    new XElement("TotalMessages", totalMessages)
                );
                Console.WriteLine(luggageMessage);
                mqLuggage.Send(luggageMessage.ToString(), "Luggage Information");
            }

            // Resequencer
            Console.WriteLine("\nResequencing...\n");
            Resequencer resequencer = new Resequencer();
            resequencer.ProcessMessages();

            // Aggregator
            Console.WriteLine("\nAggregating...\n");
            Aggregator aggregator = new Aggregator();
            aggregator.AggregateMessages();

            Console.ReadLine();
        }
    }
}
