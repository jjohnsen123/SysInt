using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lek13Opgaver
{
    public class SasFlightTransformation : IFlightTransformation
    {
        private readonly AirlineCompanySAS _sasFlight;

        public SasFlightTransformation(AirlineCompanySAS sasFlight)
        {
            _sasFlight = sasFlight;
        }

        public FlightInfo Transform()
        {
            var flightInfo = new FlightInfo
            {
                Airline = _sasFlight.Airline,
                FlightNo = _sasFlight.FlightNo,
                Destination = _sasFlight.Destination,
                Origin = _sasFlight.Origin,
                FlightType = _sasFlight.ArivalDeparture,
                DateTime = ParseDateTime(_sasFlight.Date, _sasFlight.Time)
            };
            return flightInfo;
        }

        private DateTime ParseDateTime(string date, string time)
        {
            var dateString = date.Replace("marts", "March");
            DateTime parsedDate = DateTime.ParseExact($"{dateString} {time}", "d. MMMM yyyy HH:mm", CultureInfo.InvariantCulture);
            return parsedDate;
        }
    }
}
