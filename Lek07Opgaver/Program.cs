using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lek07Opg2
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Lektion 07 opgave 2
            var flightInfo1 = new FlightInfo
            {
                FlightNumber = "AB123",
                Airline = "Airline Name 1",
                Origin = "JFK",
                Destination = "Bluff City Airport",
                EstimatedArrivalTime = DateTime.Now.AddHours(2), // Fly ankommer om 2 timer
                CurrentPosition = new Position { Latitude = 40.7128, Longitude = -74.0060 },
                Altitude = 32000, // Flyets højde i fod
                Speed = 540, // Flyets hastighed i knots
                Status = "On Time"
            };

            var flightInfo2 = new FlightInfo
            {
                FlightNumber = "CD456",
                Airline = "Airline Name 2",
                Origin = "LAX",
                Destination = "Bluff City Airport",
                EstimatedArrivalTime = DateTime.Now.AddHours(3), // Fly ankommer om 3 timer
                CurrentPosition = new Position { Latitude = 34.0522, Longitude = -118.2437 },
                Altitude = 35000, // Flyets højde i fod
                Speed = 510, // Flyets hastighed i knots
                Status = "Delayed"
            };

            string jsonFlightInfo1 = JsonConvert.SerializeObject(flightInfo1);
            string jsonFlightInfo2 = JsonConvert.SerializeObject(flightInfo2);


            MessageQueue messageQueue = null;
            if (!MessageQueue.Exists(@".\Private$\FlightInfoBluff"))
            {
                MessageQueue.Create(@".\Private$\FlightInfoBluff");
            }

            messageQueue = new MessageQueue(@".\Private$\FlightInfoBluff");
            messageQueue.Send(jsonFlightInfo1, "flightInfo1"); //Header skal være DEST_QUEUE: KøId til Destination 
            messageQueue.Send(jsonFlightInfo2, "flightInfo2"); //og RESP_QUEUE: KøId til Response
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
