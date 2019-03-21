using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public partial class Employee

    {

        public Employee()

        {

            InverseSupervisorPerson = new HashSet<Employee>();

        }



        public int PersonId { get; set; }

        public int? EmployeeNo { get; set; }

        public DateTime? HireDate { get; set; }

        public int? SupervisorPersonId { get; set; }



        public Person Person { get; set; }

        public Employee SupervisorPerson { get; set; }

        public Pilot Pilot { get; set; }

        public ICollection<Employee> InverseSupervisorPerson { get; set; }

    }
}
