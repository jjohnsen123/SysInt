using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opgave2
{
    public class ETA
    {
        public string Airline { get; set; }
        public string FlightNo { get; set; }
        public string Aircraft { get; set; }
        public string Track { get; set; }
        public string Estimated_Arrival { get; set; }

        public ETA(string airline, string flightNo, string aircraft, string track, string estimated_Arrival)
        {
            Airline = airline;
            FlightNo = flightNo;
            Aircraft = aircraft;
            Track = track;
            Estimated_Arrival = estimated_Arrival;
        }

        public ETA() { }

        public override string ToString()
        {
            return $"Airline: {Airline}, FlightNo: {FlightNo}, Aircraft: {Aircraft}, Track: {Track}, ETA: {Estimated_Arrival}";
        }
    }
    }
