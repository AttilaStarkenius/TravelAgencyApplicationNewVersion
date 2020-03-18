using System;
using System.Collections.Generic;

namespace TravelAgencyApplicationNewVersion.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Reservation = new HashSet<Reservation>();
        }

        public int CustomerId { get; set; }
        public bool Member { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
