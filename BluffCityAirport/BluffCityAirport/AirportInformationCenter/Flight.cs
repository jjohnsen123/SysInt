using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluffCityAirport.AirportInformationCenter
{
    internal class Flight
    {
        public string airline {  get; set; }
        public string flightNo { get; set; }
        public DateTime plannedTime { get; set; }
        public string type { get; set; }
        public string destination { get; set; }
        public string checkIn { get; set; }

    }
}
