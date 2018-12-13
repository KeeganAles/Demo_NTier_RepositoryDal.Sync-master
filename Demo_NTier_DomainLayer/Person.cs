using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_NTier_DomainLayer
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public double WorkPhone { get; set; }
        public double HomePhone { get; set; }
        public double CellPhone { get; set; }
        public string WorkEmail { get; set; }
        public string HomeEmail { get; set; }

        public string FullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
