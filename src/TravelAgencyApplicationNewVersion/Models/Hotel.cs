using System;
using System.Collections.Generic;

namespace TravelAgencyApplicationNewVersion.Models
{
    public partial class Hotel
    {
        public Hotel()
        {
            ReservationDetails = new HashSet<ReservationDetails>();
        }

        public int RoomId { get; set; }
        public double Price { get; set; }
        public int HotelId { get; set; }
        public string City { get; set; }
        public string HotelName { get; set; }

        public virtual ICollection<ReservationDetails> ReservationDetails { get; set; }
    }
}
