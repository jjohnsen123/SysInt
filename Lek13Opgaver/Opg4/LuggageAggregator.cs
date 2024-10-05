using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lek13Opgaver.Opg4
{
    public class LuggageAggregator
    {
        public decimal GetTotalWeightForPassenger(XElement flightDetailsInfo, string reservationNumber)
        {
            var luggageItems = flightDetailsInfo
                .Elements("Luggage")
                .Where(l => l.Element("Id").Value.StartsWith(reservationNumber));

            return luggageItems.Sum(l => decimal.Parse(l.Element("Weight").Value));
        }

        public decimal GetTotalWeightForFlight(XElement flightDetailsInfo)
        {
            return flightDetailsInfo
                .Descendants("Luggage")
                .Sum(l => decimal.Parse(l.Element("Weight").Value));
        }
    }
}
