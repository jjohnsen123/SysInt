using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lek02Opg
{
    public class Flight
    {
        public string Airline { get; set; }
        public DateTime ScheduledDeparture { get; set; }
        public DateTime ScheduledArrival { get; set; }
        public string FlightNo { get; set; }
        public string Destination { get; set; }
        public string CheckIn { get; set; }

    }
}
