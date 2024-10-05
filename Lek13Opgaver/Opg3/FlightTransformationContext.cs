using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lek13Opgaver
{
    public class FlightTransformationContext
    {
        private readonly IFlightTransformation _flightTransformation;

        public FlightTransformationContext(IFlightTransformation flightTransformation)
        {
            _flightTransformation = flightTransformation;
        }

        public FlightInfo ExecuteTransformation()
        {
            return _flightTransformation.Transform();
        }
    }
}
