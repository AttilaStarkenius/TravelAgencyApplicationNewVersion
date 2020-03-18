using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyApplicationNewVersion.ViewModels;
using TravelAgencyApplicationNewVersion.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace TravelAgencyApplicationNewVersion.Controllers
{
    public class SearchController : Controller
    {
        
        public IActionResult Index()
        {
            try
            {

                
                SearchIndexViewModel searchModel = new SearchIndexViewModel();


                searchModel.flightTypes = new List<FlightTypeViewModel>
        {
            new FlightTypeViewModel {Id = 1, FlightTypeName = "Return flight"},
            new FlightTypeViewModel {Id = 2, FlightTypeName = "Single flight"}
        };


                var db = new TravelAgencyContext();

                

                searchModel.hotelCityItems = db.Hotel
                .GroupBy(h => h.City)
                .Select(g => new SelectListItem
                {
                    Value = g.Key.ToString(),
                    Text = g.First().City
                });

                searchModel.flightCityItems = db.Flight
                .GroupBy(h => h.DepartureCity)
                .Select(g => new SelectListItem
                {
                    Value = g.Key.ToString(),
                    Text = g.First().DepartureCity
                });

                searchModel.flightArrivalCityItems = db.Flight
                .GroupBy(h => h.ArrivalCity)
                .Select(g => new SelectListItem
                {
                    Value = g.Key.ToString(),
                    Text = g.First().ArrivalCity
                });


                searchModel.hotelCities = new SelectList(searchModel.hotelCityItems, "HotelCity", "HotelCity");



                List<SelectListItem> States = new List<SelectListItem>()
                {
                    new SelectListItem() {Text="London", Value="LO"},
                    new SelectListItem() { Text="Moscow", Value="MO"}
                };

                ViewBag.HotelCities = States;

                





                return View("Search", searchModel);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);                

                return View("Search");

            }

        }


        public IActionResult SearchFlight(string date, string arrivaldate, SearchIndexViewModel model)
        {
            try
            {

                var context = new TravelAgencyContext();

                //model.FlightPriceList = (from b in context.Flight
                //                              where b.DepartureTime.Date.Equals(model.DepartureTime)                                              
                //                              select g.First());




                //model.FlightPrice = model.numberOfAdults * model.FlightPrice;

                model.flightsDepartureAndArrivalCity = (from b in context.Flight
                                              where b.DepartureCity == model.DepartureCity && b.ArrivalCity == model.ArrivalCity
                                              group b by b.FlightId into g
                                              select g.First()).ToList();



                if (date != null)
                {
                    model.DepartureTime = DateTime.Parse(date);
                }

                if(arrivaldate != null)
                {
                    model.ArrivalTime = DateTime.Parse(arrivaldate);
                }


                if(model.ArrivalTime == DateTime.MinValue) { 
                model.flightsDepartureDate = (from b in context.Flight
                                              where b.DepartureTime.Date.Equals(model.DepartureTime) && b.DepartureCity == model.DepartureCity && b.ArrivalCity == model.ArrivalCity
                                              group b by b.FlightId into g
                                              select g.First()).ToList();
                }
                else
                {
                    model.flightsDepartureDate = (from b in context.Flight
                     where b.DepartureTime.Date.Equals(model.DepartureTime) && b.ArrivalTime.Date.Equals(model.ArrivalTime) && b.DepartureCity == model.DepartureCity && b.ArrivalCity == model.ArrivalCity
                                                  group b by b.FlightId into g
                     select g.First()).ToList();
                }

                if (!model.flightsDepartureDate.Count.Equals(0)) { 
                var flight = model.flightsDepartureDate.First();

                model.FlightPrice = ((model.numberOfAdults * flight.FlightPrice) + (model.numberOfChildren * flight.FlightPrice) / 2);
                }

                if (model.selectedFlightType.Equals(1))
                {

                    //model.FlightPrice = model.FlightPrice * 2; 

                    model.TotalPrice = model.FlightPrice * 2;

                    model.flightsArrivalDate = (from b in context.Flight
                                                where b.ArrivalCity == model.DepartureCity && b.DepartureCity == model.ArrivalCity
                                                group b by b.FlightId into g
                                                select g.First()).ToList();

                }

                else { 

                model.TotalPrice = model.FlightPrice;

                    model.flightsArrivalDate = new List<Flight>();


                }


                //model.flightsArrivalDate = (from b in context.Flight
                //                            where b.ArrivalTime.Date.Equals(model.ArrivalTime)
                //                            group b by b.FlightId into g
                //                            select g.First()).ToList();


                //model.flightsWithDepartureAndArrivalDate = (from b in context.Flight
                //                                            where b.DepartureTime.Date.Equals(model.DepartureTime) && b.ArrivalTime.Date.Equals(model.ArrivalTime)
                //                                            group b by b.FlightId into g
                //                                            select g.First()).ToList();


                return View(model);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }


        public IActionResult SearchFlightCity(SearchIndexViewModel model)
        {

            try
            {
                TravelAgencyContext context = new TravelAgencyContext();

               

                model.flightsDepartureCity = (from b in context.Flight
                                              where b.DepartureCity == model.DepartureCity
                                              group b by b.FlightId into g
                                              select g.First()).ToList();

                return View(model);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return View();
            }

        }



        public IActionResult Redirect(Flight model)
        {

            try { 

                return View("~/Views/Book/Book.cshtml", model);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

            

        }



        public IQueryable<Hotel> GetHotelModel(SearchIndexViewModel model)
        {

            TravelAgencyContext context = new TravelAgencyContext();
            var hotels = (from c in context.Hotel
                          where c.City.Equals(model.City)
                          select c);

            return hotels;
        }


        public IActionResult SearchHotelCity(SearchIndexViewModel model)
        {

            try
            {

                TravelAgencyContext context = new TravelAgencyContext();
                model.hotelsCity = (from b in context.Hotel
                                    where b.City == model.City
                                    group b by b.HotelId into g
                                    select g.First()).ToList();
                return View(model);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }
        }



        public IActionResult SearchDepartureDate(string date, SearchIndexViewModel model)
        {


            try
            {

                model.DepartureTime = DateTime.Parse(date);

                //model.DepartureTime = DateTime.Parse(startDate);

                //model.DepartureTime = DateTime.Parse(hiddenFieldID);

                //model.DepartureTime = hiddenFieldID;

                TravelAgencyContext context = new TravelAgencyContext();


                model.flightsDepartureDate = (from b in context.Flight
                                              where b.DepartureTime.Date.Equals(model.DepartureTime)
                                                group b by b.FlightId into g
                                              select g.First()).ToList();

                return View(model);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }



        public IActionResult SearchArrivalDate(SearchIndexViewModel model)
        {


            try
            {

                TravelAgencyContext context = new TravelAgencyContext();


                model.flightsArrivalDate = (from b in context.Flight
                                            where b.ArrivalTime.Date.Equals(model.ArrivalTime)
                                            group b by b.FlightId into g
                                              select g.First()).ToList();

                return View(model);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }




        public IActionResult SearchResults(SearchIndexViewModel model)
        {
            try
            {

                TravelAgencyContext context = new TravelAgencyContext();


                

                model.flight = (from c in context.Flight
                               where c.DepartureTime.Date.Equals(model.DepartureTime)
                               orderby c.SeatId
                               select c).First();

                

                return View(model);

            }



            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }



        }

        

        

    }
}
