using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    public partial class Person

    {

        public int PersonId { get; set; }

        public string Surname { get; set; }

        public string GivenName { get; set; }

        public string Country { get; set; }

        public DateTime? Birthday { get; set; }

        public byte[] Photo { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public string Memo { get; set; }



        public Employee Employee { get; set; }

        public Passenger Passenger { get; set; }

    }
}
