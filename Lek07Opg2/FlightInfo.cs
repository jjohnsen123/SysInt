using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lek07Opg2
{
    public class FlightInfo
    {
        public string FlightNumber { get; set; }
        public string Airline { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime EstimatedArrivalTime { get; set; }
        public Position CurrentPosition { get; set; }
        public int Altitude { get; set; } // In feet
        public int Speed { get; set; } // In knots
        public string Status { get; set; } // On Time, Delayed, etc.

        public override string ToString()
        {
            return $"Flight Number: {FlightNumber}, Airline: {Airline}, Origin: {Origin}, Destination: {Destination}, " +
                   $"Estimated Arrival Time: {EstimatedArrivalTime.ToString("yyyy-MM-dd HH:mm:ss")}, " +
                   $"Current Position: Latitude {CurrentPosition.Latitude}, Longitude {CurrentPosition.Longitude}, " +
                   $"Altitude: {Altitude} feet, Speed: {Speed} knots, Status: {Status}";
        }

    }

    public class Position
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
