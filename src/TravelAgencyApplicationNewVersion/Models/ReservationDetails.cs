using System;
using System.Collections.Generic;

namespace TravelAgencyApplicationNewVersion.Models
{
    public partial class ReservationDetails
    {
        public int ReservationDetailsId { get; set; }
        public int ReservationId { get; set; }
        public DateTime BookingDate { get; set; }
        public bool Insurance { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public double Price { get; set; }
        public bool? FirstClass { get; set; }
        public int? SeatId { get; set; }
        public DateTime InvoiceExpiryDate { get; set; }
        public int? RoomId { get; set; }
        public bool? Breakfast { get; set; }
        public string Email { get; set; }

        public virtual Reservation Reservation { get; set; }
        public virtual Hotel Room { get; set; }
        public virtual Flight Seat { get; set; }
    }
}
