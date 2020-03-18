using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelAgencyApplicationNewVersion.Models
{
    public partial class Flight
    {
        public Flight()
        {
            ReservationDetails = new HashSet<ReservationDetails>();
        }


        public int SeatId { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }

        [Required(ErrorMessage = "Departure time is required")]
        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }
        public int FlightId { get; set; }
        public string Airline { get; set; }
        public bool FirstClass { get; set; }


        public double FlightPrice { get; set; }



        public virtual ICollection<ReservationDetails> ReservationDetails { get; set; }
    }
}
