using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    public partial class Pilot

    {

        public Pilot()

        {

            Flight = new HashSet<Flight>();

        }



        public int PersonId { get; set; }

        public DateTime? LicenseDate { get; set; }

        public string Flightscheintyp { get; set; }

        public int? FlightHours { get; set; }

        public string FlightSchool { get; set; }



        public Employee Person { get; set; }

        public ICollection<Flight> Flight { get; set; }

    }
}
