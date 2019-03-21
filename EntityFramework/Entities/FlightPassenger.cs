using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public partial class FlightPassenger

    {

        public int FlightFlightNo { get; set; }

        public int PassengerPersonId { get; set; }



        public Flight FlightFlightNoNavigation { get; set; }

        public Passenger PassengerPerson { get; set; }

    }
}
