using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TravelAgencyApplicationNewVersion.Models;

namespace TravelAgencyApplicationNewVersion.ViewModels
{
    public class BookViewModel
    {

        public RegisterVIewModel RegisterVIewModel { get; set; }
        public LogInViewModel LogInViewModel { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
        public IQueryable<Flight> result { get; set; }

        public IQueryable<Flight> resultReturnFlight { get; set; }


        public List<Flight> allSeatsInFlight { get; set; }

        public List<Flight> allSeatsInFlightReturnFlight { get; set; }


        public List<int> seatsInReservationDetailsTable { get; set; }

        public List<int> seatsInReservationDetailsTableReturnFlight { get; set; }


        public IEnumerable<Hotel> roomDetails { get; set; }

        public List<Flight> flightsArrivalDate { get; set; }


        public IEnumerable<Flight> seatDetails { get; set; }


        public IQueryable<SelectListItem> roomsInHotel { get; set; }


        public IQueryable<SelectListItem> seatsInFlight { get; set; }

        public IQueryable<SelectListItem> seatsInReturnFlight { get; set; }



        public IEnumerable<int> chosenFlightDetails { get; set; }



        public int ReturnFlightSeatId { get; set; }



        public IQueryable<int> seats { get; set; }


        public Flight flight { get; set; }
        public Hotel hotel { get; set; }

        

        public int FlightId { get; set; }

        public int HotelId { get; set; }



        public int SeatId { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Airline { get; set; }
        public bool FirstClass { get; set; }

        public int RoomId { get; set; }
        public double Price { get; set; }
        public string City { get; set; }
        public string HotelName { get; set; }
        public int ReturnFlightID { get; internal set; }
    }
}
