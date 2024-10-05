using Lek13Opgaver.Opg4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lek13Opgaver
{
    public class Program
    {
        static void Main(string[] args)
        {
            var sasFlight = new AirlineCompanySAS
            {
                Airline = "SAS",
                FlightNo = "SK 239",
                Destination = "JFK",
                Origin = "CPH",
                ArivalDeparture = "D",
                Date = "6. marts 2017",
                Time = "16:45"
            };


            IFlightTransformation sasTranform = new SasFlightTransformation(sasFlight);
            FlightTransformationContext context = new FlightTransformationContext(sasTranform);

            FlightInfo canonicalFlight = context.ExecuteTransformation();

            Console.WriteLine("Original:");
            Console.WriteLine($"Airline: {sasFlight.Airline}");
            Console.WriteLine($"Flight No: {sasFlight.FlightNo}");
            Console.WriteLine($"From: {sasFlight.Origin} To: {sasFlight.Destination}");
            Console.WriteLine($"Flight Type: {sasFlight.ArivalDeparture}");
            Console.WriteLine($"Date: {sasFlight.Date}");
            Console.WriteLine($"Time: {sasFlight.Time}");


            Console.WriteLine("\nTransformed:");
            Console.WriteLine($"Airline: {canonicalFlight.Airline}");
            Console.WriteLine($"Flight No: {canonicalFlight.FlightNo}");
            Console.WriteLine($"From: {canonicalFlight.Origin} To: {canonicalFlight.Destination}");
            Console.WriteLine($"Flight Type: {canonicalFlight.FlightType}");
            Console.WriteLine($"Date and Time: {canonicalFlight.DateTime}");


            // Opg 5
            // Doesn't exist. LOL!
            XDocument xmlDoc = XDocument.Load("FlightDetailsInfoResponse.xml");

            var aggregator = new LuggageAggregator();

            // Get total weight for a specific passenger
            decimal passengerWeight = aggregator.GetTotalWeightForPassenger(xmlDoc.Root, "CA937200305251");
            Console.WriteLine($"Total luggage weight for passenger: {passengerWeight}");

            // Get total weight for all passengers on the flight
            decimal totalFlightWeight = aggregator.GetTotalWeightForFlight(xmlDoc.Root);
            Console.WriteLine($"Total luggage weight for the flight: {totalFlightWeight}");

            Console.ReadLine();
        }
    }
}
