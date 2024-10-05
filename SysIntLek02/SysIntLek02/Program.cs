using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using Newtonsoft.Json;

namespace Lek02Opg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Lektion 02
            Flight flight1 = new Flight
            {
                Airline = "SAS",
                ScheduledDeparture = DateTime.Parse("2024-02-06T13:00:00"),
                ScheduledArrival = DateTime.Parse("2024-02-06T15:00:00"),
                FlightNo = "SK123",
                Destination = "Copenhagen",
                CheckIn = "Gate 5"
            };
            Flight flight2 = new Flight
            {
                Airline = "Lufthansa",
                ScheduledArrival = DateTime.Parse("2024 - 02 - 06T14: 30:00"),
                ScheduledDeparture = DateTime.Parse("2024-02-06T17:00:00"),
                FlightNo = "LH456",
                Destination = "Frankfurt",
                CheckIn = "Gate7"
            };

            string json1 = JsonConvert.SerializeObject(flight1);
            string json2 = JsonConvert.SerializeObject(flight2);

            MessageQueue messageQueue = null;
            if (!MessageQueue.Exists(@".\Private$\FlightBluff"))
            {
                MessageQueue.Create(@".\Private$\FlightBluff");
            }

            messageQueue = new MessageQueue(@".\Private$\FlightBluff");
            messageQueue.Send(json1, "flight1"); //Header skal være DEST_QUEUE: KøId til Destination 
            messageQueue.Send(json2, "flight2"); //og RESP_QUEUE: KøId til Response
            Console.WriteLine("Besked sendt til MSMQ.");


            while (true)
           {
                try
                {
                    messageQueue.Peek(new TimeSpan(0, 0, 1));

                    Message recMsg = messageQueue.Receive(new TimeSpan(0, 0, 1));
                    recMsg.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });

                    string msgBody = recMsg.Body.ToString();
                    string msgLbl = recMsg.Label;

                    Console.WriteLine("Besked modtaget fra MSMQ:");
                    Console.WriteLine($"Titel: {msgLbl}");
                    Console.WriteLine($"Body: {msgBody}");
                }
                catch (MessageQueueException mqe)
                {
                    if (mqe.MessageQueueErrorCode == MessageQueueErrorCode.IOTimeout)
                    {
                        Console.WriteLine("Ingen flere beskeder i køen.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"MessageQueueException: {mqe.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }

            Console.ReadLine();
        }
    }
}
