using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TravelAgencyApplicationNewVersion.Models;

namespace TravelAgencyApplicationNewVersion.ViewModels
{
    


    public class SearchIndexViewModel : Flight, IHotel
        {



        public enum HotelCity
        {
            London,
            Moscow
        }



        //public List<double> FlightPriceList { get; set; }

        public double TotalPrice { get; set; }

        public IEnumerable<FlightTypeViewModel> flightTypes { set; get; }
        public int selectedFlightType { set; get; }



        public int numberOfAdults { get; set; }


        public int numberOfChildren { get; set; }


        public List<Hotel> hotelsCity { get; set; }


        public List<Flight> flightsWithDepartureAndArrivalDate { get; set; }


        public List<Flight> flightsArrivalDate { get; set; }


        public List<Flight> flightsDepartureDate { get; set; }


        public List<Flight> flightsDepartureAndArrivalCity { get; set; }


        public List<Flight> flightsDepartureCity { get; set; }


        public IQueryable<ReservationDetails> reservationDetailsItems { get; set; }

        public IOrderedQueryable<ReservationDetails> reservationDetailItems { get; set; }


        public IOrderedQueryable<Reservation> reservationItems { get; set; }


        public HotelCity hotelCity { get; set; }


        public IQueryable<SelectListItem> flightArrivalCityItems { get; set; }


        public IQueryable<SelectListItem> flightCityItems { get; set; }

        public IQueryable<SelectListItem> hotelCityItems { get; set; }

        public SelectList hotelCities { get; set; }


        public List<Flight> flights { get; set; }
        public List<Hotel> hotels { get; set; }



        Flight model1;
            Hotel model2;

            Hotel IHotel.GetHotel
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public ReservationDetails reservationDetails { get; set; }

            public Flight flight { get; set; }
            public Hotel hotel { get; set; }

            public IQueryable<Hotel> hotelsQuery { get; set; }
            public IQueryable<Flight> flightsQuery { get; set; }

            
            private IQueryable<string> queryable;

            public SearchIndexViewModel(IQueryable<string> queryable)
            {
                this.queryable = queryable;
            }

            public SearchIndexViewModel()
            {

            }

            
            public string SelectedDepartureCity { get; set; }

            
            public IEnumerable<SelectListItem> DepartureCities
            {
            get; set;
                
                
            }

            public IQueryable<Flight> searchResultFlights { get; set; }

            public int Id { get; set; }           
            public double Price { get; set; }            
            public int HotelId { get; set; }
            public string City { get; set; }
            public string HotelName { get; set; }
            public int RoomId { get; set; }

    }

    }

