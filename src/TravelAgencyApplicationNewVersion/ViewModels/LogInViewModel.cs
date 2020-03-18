using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgencyApplicationNewVersion.Models;

namespace TravelAgencyApplicationNewVersion.ViewModels
{
    public class LogInViewModel
    {

        public Flight flight { get; set; }
        public Hotel hotel { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public IQueryable<int> seats { get; internal set; }
        public List<Flight> flightsArrivalDate { get; internal set; }
        public List<int> seatsInReservationDetailsTableReturnFlight { get; internal set; }
        public List<Flight> allSeatsInFlightReturnFlight { get; internal set; }
        public int ReturnFlightID { get; internal set; }
        public IQueryable<Flight> resultReturnFlight { get; internal set; }
        public IQueryable<SelectListItem> seatsInReturnFlight { get; internal set; }
        public List<int> seatsInReservationDetailsTable { get; internal set; }
        public List<Flight> allSeatsInFlight { get; internal set; }
        public int FlightId { get; internal set; }
        public IQueryable<Flight> result { get; internal set; }
        public IQueryable<SelectListItem> seatsInFlight { get; internal set; }
    }
}
