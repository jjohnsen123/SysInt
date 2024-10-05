using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lek13Opgaver
{
    public class FlightInfo
    {
        public string Airline { get; set; }
        public string FlightNo { get; set; }
        public string Destination { get; set; }
        public string Origin { get; set; }
        public string FlightType { get; set; } // "A" for Arrival, "D" for Departure
        public DateTime DateTime { get; set; } // Combined date and time
    }
}
