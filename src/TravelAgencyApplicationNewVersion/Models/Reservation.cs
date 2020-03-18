using System;
using System.Collections.Generic;

namespace TravelAgencyApplicationNewVersion.Models
{
    public partial class Reservation
    {
        public Reservation()
        {
            ReservationDetails = new HashSet<ReservationDetails>();
        }

        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public double TotalPrice { get; set; }

        public virtual ICollection<ReservationDetails> ReservationDetails { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
