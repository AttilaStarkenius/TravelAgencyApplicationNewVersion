using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyApplicationNewVersion.ViewModels;
using TravelAgencyApplicationNewVersion.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TravelAgencyApplicationNewVersion.Services;
using Microsoft.EntityFrameworkCore;

namespace TravelAgencyApplicationNewVersion.Controllers
{
    public class BookController : Controller
    {

        private readonly TravelAgencyContext _context;

        public BookController(TravelAgencyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            try
            {

                BookViewModel model = new BookViewModel();

                model.FlightId = (int)TempData["ChosenFlight"];

                //if((int)TempData["ChosenReturnFlight"] != 0)
                //{
                model.ReturnFlightID = (int)TempData["ChosenReturnFlight"];
                //}

                


                TravelAgencyContext context = new TravelAgencyContext();
                              
                

                model.flight = (from c in context.Flight
                                where c.FlightId.Equals(model.FlightId)
                                orderby c.SeatId
                                select c).First();

                model.flight.FlightPrice = (double)TempData["FlightPrice"];


                //if (model.ReturnFlightID != 0) { 
                    model.flightsArrivalDate = (from b in context.Flight
                                            where b.ArrivalCity == model.flight.DepartureCity && b.DepartureCity == model.flight.ArrivalCity
                                            group b by b.FlightId into g
                                            select g.First()).ToList();
                //}
                //else
                //{
                    //model.flightsArrivalDate = new List<Flight>();

                //}

                model.chosenFlightDetails = (from b in context.Flight
                                       where b.FlightId.Equals(model.FlightId)
                                       select b.SeatId).ToList().AsEnumerable();

                

                model.seatsInReservationDetailsTable = (from c in context.ReservationDetails
                                where c.SeatId != null
                                orderby c.SeatId
                                select c.SeatId ?? default(int)).ToList();

                model.allSeatsInFlight = context.Flight
                .Where((c => c.FlightId == model.FlightId)).ToList();


                model.result = model.allSeatsInFlight.Where(p => !model.seatsInReservationDetailsTable.Any(p2 => p2.Equals(p.SeatId))).AsQueryable();


                model.seatsInFlight = model.result
                .GroupBy(h => h.SeatId)
                .Select(g => new SelectListItem
                {
                    Value = g.Key.ToString(),
                    Text = g.First().SeatId.ToString()
                });



                model.seatsInReservationDetailsTableReturnFlight = (from c in context.ReservationDetails
                                                                    where c.SeatId != null
                                                                    orderby c.SeatId
                                                                    select c.SeatId ?? default(int)).ToList();

                model.allSeatsInFlightReturnFlight = context.Flight
                .Where((c => c.FlightId == model.ReturnFlightID)).ToList();


                model.resultReturnFlight = model.allSeatsInFlightReturnFlight.Where(p => !model.seatsInReservationDetailsTableReturnFlight.Any(p2 => p2.Equals(p.SeatId))).AsQueryable();


                model.seatsInReturnFlight = model.resultReturnFlight
                .GroupBy(h => h.SeatId)
                .Select(g => new SelectListItem
                {
                    Value = g.Key.ToString(),
                    Text = g.First().SeatId.ToString()
                });

                return View("Index", model);

            }


            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                
                return View();

            }

        }



        public IActionResult BookRoom(int ReturnFlightSeatIdID, int flightID, int ID, double FlightPrice, BookViewModel model)
        {

            try
            {

                model.ReturnFlightSeatId = ReturnFlightSeatIdID;

                //BookViewModel model = new BookViewModel();                

                //model.HotelId = (int)TempData["ChosenHotel"];


                TravelAgencyContext context = new TravelAgencyContext();

                //var flightID = flightID;

                model.flight = (from c in context.Flight
                                where c.FlightId.Equals(flightID)
                                orderby c.SeatId
                                select c).First();


                model.flight.SeatId = ID;

                //model.seatDetails = (from b in context.Flight
                //                     where b.SeatId.Equals(model.flight.SeatId)
                //                     select b);

                //var actualFlightID = (from c in context.Flight
                //                      where c.SeatId.Equals(model.flight.SeatId)
                //                      orderby c.SeatId
                //                      select c.FlightId).First();

                

                

                


                model.flight.FlightPrice = FlightPrice;

                model.flightsArrivalDate = (from b in context.Flight
                                            where b.ArrivalCity == model.flight.DepartureCity && b.DepartureCity == model.flight.ArrivalCity
                                            group b by b.FlightId into g
                                            select g.First()).ToList();



                //model.hotel.City = model.flight.ArrivalCity;

                model.hotel = (from c in context.Hotel
                               where c.City.Equals(model.flight.ArrivalCity)
                               orderby c.HotelId
                               select c).First();



                //model.hotel = (from c in context.Hotel
                //               where c.HotelId.Equals(model.HotelId)
                //               orderby c.RoomId
                //               select c).First();

                model.roomsInHotel = context.Hotel
               .Where(c => c.HotelId == model.hotel.HotelId)
               .GroupBy(h => h.RoomId)
               .Select(g => new SelectListItem
               {
                   Value = g.Key.ToString(),
                   Text = g.First().RoomId.ToString()
               });

                              


                return View("BookRoom", model);

            }


            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }





        public IActionResult LoginOrRegisterHotel(int ID)
        {

            try
            {

                BookViewModel model = new BookViewModel();

                model.HotelId = ID;

                ViewBag.HotelId = ID;


                ViewData["HotelId"] = ID;

                TravelAgencyContext context = new TravelAgencyContext();








                model.hotel = (from c in context.Hotel
                                where c.HotelId.Equals(model.HotelId)
                                orderby c.RoomId
                                select c).First();



                


                LogInViewModel logInModel = new LogInViewModel();


                logInModel.hotel = (from c in context.Hotel
                                    where c.HotelId.Equals(model.HotelId)
                                    orderby c.RoomId
                                    select c).First();


                

                ViewData["ChosenHotel"] = model.hotel;

                if (User.Identity.IsAuthenticated)
                {

                    model.roomsInHotel = context.Hotel
                .Where(c => c.HotelId == model.HotelId)
                .GroupBy(h => h.RoomId)
                .Select(g => new SelectListItem
                {
                    Value = g.Key.ToString(),
                    Text = g.First().RoomId.ToString()
                });

                    return View("BookRoom", model);

                }

                else
                {

                    return View("LoginOrRegisterHotel", logInModel);

                }


            }


            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }



        public IActionResult LoginOrRegister(int returnFlightID, int ID , double flightPrice)
        {

            try
            {

                BookViewModel model = new BookViewModel();

                model.FlightId = ID;

                if(returnFlightID != 0) { 
                model.ReturnFlightID = returnFlightID;
                }

                //model.flight.FlightPrice = flightPrice;

                ViewBag.FlightId = ID;


                ViewData["FlightId"] = ID;

                TravelAgencyContext context = new TravelAgencyContext();

                model.flight = (from c in context.Flight
                                where c.FlightId.Equals(model.FlightId)
                                orderby c.SeatId
                                select c).First();

                model.flightsArrivalDate = (from c in context.Flight
                                            where c.ArrivalCity == model.flight.DepartureCity && c.DepartureCity == model.flight.ArrivalCity
                                            group c by c.FlightId into g
                                            select g.First()).ToList();

                model.flight.FlightPrice = flightPrice;



                model.seats = (from c in context.Flight
                               where c.FlightId.Equals(model.FlightId)
                               orderby c.SeatId
                               select c.SeatId);








                LogInViewModel logInModel = new LogInViewModel();


                logInModel.flight = (from c in context.Flight
                                where c.FlightId.Equals(model.FlightId)
                                orderby c.SeatId
                                select c).First();

                logInModel.flightsArrivalDate = (from c in context.Flight
                                            where c.ArrivalCity == model.flight.DepartureCity && c.DepartureCity == model.flight.ArrivalCity
                                            group c by c.FlightId into g
                                            select g.First()).ToList();

                logInModel.flight.FlightPrice = flightPrice;



                logInModel.seats = (from c in context.Flight
                               where c.FlightId.Equals(model.FlightId)
                               orderby c.SeatId
                               select c.SeatId);




                ViewData["ChosenFlight"] = model.flight;

                //if (User.Identity.IsAuthenticated) {

                    if(model.ReturnFlightID != 0)
                    {
                        model.seatsInReservationDetailsTableReturnFlight = (from c in context.ReservationDetails
                                                                where c.SeatId != null
                                                                orderby c.SeatId
                                                                select c.SeatId ?? default(int)).ToList();

                        model.allSeatsInFlightReturnFlight = context.Flight
                        .Where((c => c.FlightId == model.ReturnFlightID)).ToList();


                        model.resultReturnFlight = model.allSeatsInFlightReturnFlight.Where(p => !model.seatsInReservationDetailsTableReturnFlight.Any(p2 => p2.Equals(p.SeatId))).AsQueryable();


                        model.seatsInReturnFlight = model.resultReturnFlight
                        .GroupBy(h => h.SeatId)
                        .Select(g => new SelectListItem
                        {
                            Value = g.Key.ToString(),
                            Text = g.First().SeatId.ToString()
                        });


                    logInModel.seatsInReservationDetailsTableReturnFlight = (from c in context.ReservationDetails
                                                                        where c.SeatId != null
                                                                        orderby c.SeatId
                                                                        select c.SeatId ?? default(int)).ToList();

                    logInModel.allSeatsInFlightReturnFlight = context.Flight
                    .Where((c => c.FlightId == logInModel.ReturnFlightID)).ToList();


                    logInModel.resultReturnFlight = logInModel.allSeatsInFlightReturnFlight.Where(p => !logInModel.seatsInReservationDetailsTableReturnFlight.Any(p2 => p2.Equals(p.SeatId))).AsQueryable();


                    logInModel.seatsInReturnFlight = logInModel.resultReturnFlight
                    .GroupBy(h => h.SeatId)
                    .Select(g => new SelectListItem
                    {
                        Value = g.Key.ToString(),
                        Text = g.First().SeatId.ToString()
                    });
                }


                logInModel.seatsInReservationDetailsTable = (from c in context.ReservationDetails
                                                            where c.SeatId != null
                                                            orderby c.SeatId
                                                            select c.SeatId ?? default(int)).ToList();

                logInModel.allSeatsInFlight = context.Flight
                    .Where((c => c.FlightId == logInModel.FlightId)).ToList();


                logInModel.result = logInModel.allSeatsInFlight.Where(p => !logInModel.seatsInReservationDetailsTable.Any(p2 => p2.Equals(p.SeatId))).AsQueryable();


                logInModel.seatsInFlight = logInModel.result
                    .GroupBy(h => h.SeatId)
                    .Select(g => new SelectListItem
                    {
                        Value = g.Key.ToString(),
                        Text = g.First().SeatId.ToString()
                    });


                logInModel.seatsInReservationDetailsTable = (from c in context.ReservationDetails
                                                        where c.SeatId != null
                                                        orderby c.SeatId
                                                        select c.SeatId ?? default(int)).ToList();

                logInModel.allSeatsInFlight = context.Flight
                .Where((c => c.FlightId == logInModel.FlightId)).ToList();


                logInModel.result = logInModel.allSeatsInFlight.Where(p => !logInModel.seatsInReservationDetailsTable.Any(p2 => p2.Equals(p.SeatId))).AsQueryable();


                logInModel.seatsInFlight = logInModel.result
                .GroupBy(h => h.SeatId)
                .Select(g => new SelectListItem
                {
                    Value = g.Key.ToString(),
                    Text = g.First().SeatId.ToString()
                });

                if (User.Identity.IsAuthenticated)
                {
                    return View("Index", model);

                }
                //}

                else
                {

                    return View("LoginOrRegister", logInModel);

                }


            }


            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }



        public IActionResult ChooseRoomDate(double flightID, int SeatID, int ID, double flightPrice, BookViewModel model)
        {

            try
            {

                TravelAgencyContext context = new TravelAgencyContext();

                model.flight = (from c in context.Flight
                                where c.SeatId.Equals(SeatID)
                                orderby c.SeatId
                                select c).First();


                model.flight.SeatId = SeatID;

                //model.seatDetails = (from b in context.Flight
                //                     where b.SeatId.Equals(model.flight.SeatId)
                //                     select b);

                //var actualFlightID = (from c in context.Flight
                //                      where c.SeatId.Equals(model.flight.SeatId)
                //                      orderby c.SeatId
                //                      select c.FlightId).First();








                model.flight.FlightPrice = flightPrice;

                model.flightsArrivalDate = (from b in context.Flight
                                            where b.ArrivalCity == model.flight.DepartureCity && b.DepartureCity == model.flight.ArrivalCity
                                            group b by b.FlightId into g
                                            select g.First()).ToList();

                //model.DepartureTime = new DateTime();

                //model.ArrivalTime = new DateTime();

                model.RoomId = ID;


                model.roomDetails = (from b in context.Hotel
                                     where b.RoomId.Equals(model.RoomId)
                                     select b);

                var actualHotelID = (from c in context.Hotel
                                     where c.RoomId.Equals(model.RoomId)
                                     orderby c.RoomId
                                     select c.HotelId).First();

                model.hotel = (from c in context.Hotel
                               where c.HotelId.Equals(actualHotelID)
                               orderby c.RoomId
                               select c).First();


                return View(model);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }



        public IActionResult ChooseCheckOutDate(int FlightId, int SeatID, double FlightPrice, IEnumerable<Hotel> roomDetails, int RoomId, DateTime DepartureTime, BookViewModel model)
        {

            try
            {

                TravelAgencyContext context = new TravelAgencyContext();

                model.flight = (from c in context.Flight
                                where c.SeatId.Equals(SeatID)
                                orderby c.SeatId
                                select c).First();


                model.flight.SeatId = SeatID;

                //model.seatDetails = (from b in context.Flight
                //                     where b.SeatId.Equals(model.flight.SeatId)
                //                     select b);

                //var actualFlightID = (from c in context.Flight
                //                      where c.SeatId.Equals(model.flight.SeatId)
                //                      orderby c.SeatId
                //                      select c.FlightId).First();








                model.flight.FlightPrice = FlightPrice;

                model.flightsArrivalDate = (from b in context.Flight
                                            where b.ArrivalCity == model.flight.DepartureCity && b.DepartureCity == model.flight.ArrivalCity
                                            group b by b.FlightId into g
                                            select g.First()).ToList();

                model.RoomId = RoomId;

                //model.DepartureTime = CheckInDate;

                //model.RoomId = Int32.Parse(ID);


                //model.DepartureTime = ID;



                //model.RoomId = ID;


                //var checkInDate = model.DepartureTime;


                model.roomDetails = (from b in context.Hotel
                                     where b.RoomId.Equals(model.RoomId)
                                     select b);

                var actualHotelID = (from c in context.Hotel
                                     where c.RoomId.Equals(model.RoomId)
                                     orderby c.RoomId
                                     select c.HotelId).First();

                model.hotel = (from c in context.Hotel
                               where c.HotelId.Equals(actualHotelID)
                               orderby c.RoomId
                               select c).First();

                model.seatsInReservationDetailsTableReturnFlight = (from c in context.ReservationDetails
                                                                    where c.SeatId != null
                                                                    orderby c.SeatId
                                                                    select c.SeatId ?? default(int)).ToList();

                model.allSeatsInFlightReturnFlight = context.Flight
                .Where((c => c.FlightId == model.ReturnFlightID)).ToList();


                model.resultReturnFlight = model.allSeatsInFlightReturnFlight.Where(p => !model.seatsInReservationDetailsTableReturnFlight.Any(p2 => p2.Equals(p.SeatId))).AsQueryable();


                model.seatsInReturnFlight = model.resultReturnFlight
                .GroupBy(h => h.SeatId)
                .Select(g => new SelectListItem
                {
                    Value = g.Key.ToString(),
                    Text = g.First().SeatId.ToString()
                });


                return View(model);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }



        public IActionResult ChooseRoom(int ReturnFlightSeatId, int FlightId, int SeatId, double FlightPrice, IEnumerable<Hotel> roomDetails, int RoomId, DateTime DepartureTime, BookViewModel model)
        {

            try
            {

                model.ReturnFlightSeatId = ReturnFlightSeatId;

                TravelAgencyContext context = new TravelAgencyContext();

                model.flight = (from c in context.Flight
                                where c.SeatId.Equals(SeatId)
                                orderby c.SeatId
                                select c).First();


                model.flight.SeatId = SeatId;

                //model.seatDetails = (from b in context.Flight
                //                     where b.SeatId.Equals(model.flight.SeatId)
                //                     select b);

                //var actualFlightID = (from c in context.Flight
                //                      where c.SeatId.Equals(model.flight.SeatId)
                //                      orderby c.SeatId
                //                      select c.FlightId).First();








                model.flight.FlightPrice = FlightPrice;

                model.flightsArrivalDate = (from b in context.Flight
                                            where b.ArrivalCity == model.flight.DepartureCity && b.DepartureCity == model.flight.ArrivalCity
                                            group b by b.FlightId into g
                                            select g.First()).ToList();






                model.roomDetails = (from b in context.Hotel
                                     where b.RoomId.Equals(model.RoomId)
                                     select b);

                var actualHotelID = (from c in context.Hotel
                                      where c.RoomId.Equals(model.RoomId)
                                      orderby c.RoomId
                                      select c.HotelId).First();

                model.hotel = (from c in context.Hotel
                                where c.HotelId.Equals(actualHotelID)
                                orderby c.RoomId
                                select c).First();


                return View(model);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }



        public IActionResult AddHotelToBooking(double FlightPrice, BookViewModel model)
        {

            try
            {

                TravelAgencyContext context = new TravelAgencyContext();





                model.seatDetails = (from b in context.Flight
                                     where b.SeatId.Equals(model.SeatId)
                                     select b);

                var actualFlightID = (from c in context.Flight
                                      where c.SeatId.Equals(model.SeatId)
                                      orderby c.SeatId
                                      select c.FlightId).First();

                model.flight = (from c in context.Flight
                                where c.FlightId.Equals(actualFlightID)
                                orderby c.SeatId
                                select c).First();

                model.flight.FlightPrice = FlightPrice;

                //model.hotel.City = model.flight.ArrivalCity;

                model.hotel = (from c in context.Hotel
                                where c.City.Equals(model.flight.ArrivalCity)
                                orderby c.HotelId
                                select c).First();

                return View(model);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }





        public IActionResult ChooseReturnFlightSeat(int returnFlightID, double FlightPrice, BookViewModel model)
        {

            try
            {

                TravelAgencyContext context = new TravelAgencyContext();


                model.seatDetails = (from b in context.Flight
                                     where b.SeatId.Equals(model.SeatId)
                                     select b);

                var actualFlightID = (from c in context.Flight
                                      where c.SeatId.Equals(model.SeatId)
                                      orderby c.SeatId
                                      select c.FlightId).First();

                model.flight = (from c in context.Flight
                                where c.FlightId.Equals(actualFlightID)
                                orderby c.SeatId
                                select c).First();

                model.ReturnFlightID = returnFlightID;

                if (model.ReturnFlightID != 0)
                {
                    model.flightsArrivalDate = (from b in context.Flight
                                                where b.ArrivalCity == model.flight.DepartureCity && b.DepartureCity == model.flight.ArrivalCity
                                                group b by b.FlightId into g
                                                select g.First()).ToList();
                }
                else
                {
                    model.flightsArrivalDate = new List<Flight>();

                }
                

                model.flight.FlightPrice = FlightPrice;

                


                //TravelAgencyContext context = new TravelAgencyContext();

                //model.seatsInReturnFlight = seatsInReturnFlight;



                model.seatDetails = (from b in context.Flight
                                     where b.SeatId.Equals(model.SeatId)
                                     select b);

                //var actualFlightID = (from c in context.Flight
                //                      where c.SeatId.Equals(model.SeatId)
                //                      orderby c.SeatId
                //                      select c.FlightId).First();

                model.flight = (from c in context.Flight
                                where c.FlightId.Equals(actualFlightID)
                                orderby c.SeatId
                                select c).First();

                model.flightsArrivalDate = (from c in context.Flight
                                            where c.ArrivalCity == model.flight.DepartureCity && c.DepartureCity == model.flight.ArrivalCity
                                            group c by c.FlightId into g
                                            select g.First()).ToList();

                model.flight.FlightPrice = FlightPrice;


                model.seatsInReservationDetailsTableReturnFlight = (from c in context.ReservationDetails
                                                                    where c.SeatId != null
                                                                    orderby c.SeatId
                                                                    select c.SeatId ?? default(int)).ToList();

                model.allSeatsInFlightReturnFlight = context.Flight
                .Where((c => c.FlightId == model.ReturnFlightID)).ToList();


                model.resultReturnFlight = model.allSeatsInFlightReturnFlight.Where(p => !model.seatsInReservationDetailsTableReturnFlight.Any(p2 => p2.Equals(p.SeatId))).AsQueryable();


                model.seatsInReturnFlight = model.resultReturnFlight
                .GroupBy(h => h.SeatId)
                .Select(g => new SelectListItem
                {
                    Value = g.Key.ToString(),
                    Text = g.First().SeatId.ToString()
                });


                //if (returnFlightID == 0)
                //{
                //return View("ChooseSeat");
                //}
                //else
                //{
                return View(model);
                //}

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }



        public IActionResult ChooseSeat(int SeatID, double FlightPrice, BookViewModel model)
        {

            try
            {

                TravelAgencyContext context = new TravelAgencyContext();





                model.seatDetails = (from b in context.Flight
                 where b.SeatId.Equals(SeatID)                 
                 select b);

                var actualFlightID = (from c in context.Flight
                                      where c.SeatId.Equals(SeatID)
                                      orderby c.SeatId
                                      select c.FlightId).First();

                model.flight = (from c in context.Flight
                                where c.FlightId.Equals(actualFlightID)
                                orderby c.SeatId
                                select c).First();

                model.flightsArrivalDate = (from c in context.Flight
                                            where c.ArrivalCity == model.flight.DepartureCity && c.DepartureCity == model.flight.ArrivalCity
                                            group c by c.FlightId into g
                                            select g.First()).ToList();

                model.flight.FlightPrice = FlightPrice;



                return View(model);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }




        
        public IActionResult ConfirmBookingHotel(int ReturnFlightSeatId, int SeatId, double FlightPrice, int FlightId, int RoomId, DateTime DepartureTime, DateTime ArrivalTime, [Bind("ReservationDetailsId,ReservationId,BookingDate,Insurance,DepartureTime,ArrivalTime,Price,FirstClass,SeatId,InvoiceExpiryDate,RoomId,Breakfast,Email")] ReservationDetails reservationDetails)
        {

            try
            {
                

                BookViewModel model = new BookViewModel();

                model.ReturnFlightSeatId = ReturnFlightSeatId;

                TravelAgencyContext _context = new TravelAgencyContext();

                model.SeatId = SeatId;

                model.flight = (from c in _context.Flight
                                where c.SeatId.Equals(model.SeatId)
                                orderby c.SeatId
                                select c).First();

                model.flight.FlightPrice = FlightPrice;

                reservationDetails.BookingDate = DateTime.Today;
                reservationDetails.DepartureTime = model.flight.DepartureTime;
                reservationDetails.ArrivalTime = model.flight.ArrivalTime;
                //reservationDetails.Price = model.flight.FlightPrice;
                reservationDetails.FirstClass = model.flight.FirstClass;
                reservationDetails.SeatId = model.flight.SeatId;
                reservationDetails.InvoiceExpiryDate = DateTime.Today.AddDays(30);
                reservationDetails.Email = HttpContext.User.Identity.Name;

                model.RoomId = RoomId;

                model.DepartureTime = DepartureTime;

                model.ArrivalTime = ArrivalTime;



                

                var numberOfDays = model.ArrivalTime.Subtract(model.DepartureTime);


                if (numberOfDays.TotalDays < 1)
                {

                    return View("errorWrongDate", model);

                }


                model.hotel = (from c in _context.Hotel
                                where c.RoomId.Equals(model.RoomId)
                                orderby c.RoomId
                               select c).First();




                reservationDetails.BookingDate = DateTime.Today;
                //reservationDetails.Price = numberOfDays.TotalDays * model.hotel.Price;       
                reservationDetails.Price = numberOfDays.TotalDays * model.hotel.Price + model.flight.FlightPrice;
                reservationDetails.RoomId = model.hotel.RoomId;
                reservationDetails.InvoiceExpiryDate = DateTime.Today.AddDays(30);
                reservationDetails.DepartureTime = model.DepartureTime;
                reservationDetails.ArrivalTime = model.ArrivalTime;
                reservationDetails.Email = HttpContext.User.Identity.Name;


                _context.Add(reservationDetails);
                _context.SaveChanges();


                var authMessageSender = new AuthMessageSender();

                string userEmailAddress = HttpContext.User.Identity.Name;

                

                authMessageSender.SendEmailAsync(userEmailAddress, "Hey " + userEmailAddress, "Hey " + userEmailAddress + ", you've booked a seat with seatID: " +
                    model.flight.SeatId + " with the airline: " + model.flight.Airline + " from city: " + model.flight.DepartureCity + " with departure time: " + model.flight.DepartureTime +
                    " to city: " + model.flight.ArrivalCity + " with arrival time: " + model.flight.ArrivalTime +
                    " and with the price: " + model.flight.FlightPrice + " United states dollars. " +
                    "Invoice expires on the date: " + reservationDetails.InvoiceExpiryDate + " and" + userEmailAddress + " you've booked a room with roomID: " +
                    model.hotel.RoomId + " with the hotel company: " + model.hotel.HotelName +
                    " in the city: " + model.hotel.City +
                    " from date: " + model.DepartureTime + 
                    " to date: " + model.ArrivalTime +
                    " and with the price: " + numberOfDays.TotalDays * model.hotel.Price + " United states dollars. " +
                    "Invoice expires on the date: " + reservationDetails.InvoiceExpiryDate + " The total price of the booking is: " + reservationDetails.Price
                    );


                return View("ConfirmBooking");
            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }




       
        public IActionResult ConfirmBooking(double flightPrice, int ID, [Bind("ReservationDetailsId,ReservationId,BookingDate,Insurance,DepartureTime,ArrivalTime,Price,FirstClass,SeatId,InvoiceExpiryDate,RoomId,Breakfast,Email")] ReservationDetails reservationDetails)
        {

            try
            {
                

                    BookViewModel model = new BookViewModel();

                TravelAgencyContext _context = new TravelAgencyContext();

                model.SeatId = ID;

                    model.flight = (from c in _context.Flight
                                    where c.SeatId.Equals(model.SeatId)
                                    orderby c.SeatId
                                    select c).First();

                    model.flight.FlightPrice = flightPrice;

                    reservationDetails.BookingDate = DateTime.Today;
                    reservationDetails.DepartureTime = model.flight.DepartureTime;
                    reservationDetails.ArrivalTime = model.flight.ArrivalTime;
                    reservationDetails.Price = model.flight.FlightPrice;
                    reservationDetails.FirstClass = model.flight.FirstClass;
                    reservationDetails.SeatId = model.flight.SeatId;
                    reservationDetails.InvoiceExpiryDate = DateTime.Today.AddDays(30);
                    reservationDetails.Email = HttpContext.User.Identity.Name;


                _context.Add(reservationDetails);
                    _context.SaveChanges();



                var authMessageSender = new AuthMessageSender();

                //authMessageSender.SendEmailAsync("symphtom91@gmail.com", "Hey Attila!", "How are you?");

                string userEmailAddress = HttpContext.User.Identity.Name;

                authMessageSender.SendEmailAsync(userEmailAddress, "Hey " + userEmailAddress, "You've booked a seat with seatID: " +
                    model.flight.SeatId + " with the airline: " + model.flight.Airline + " from city: " + model.flight.DepartureCity + " with departure time: " + model.flight.DepartureTime +
                    " to city: " + model.flight.ArrivalCity + " with arrival time: " + model.flight.ArrivalTime + 
                    " and with the price: " + model.flight.FlightPrice + " United states dollars. " +
                    "Invoice expires on the date: " + reservationDetails.InvoiceExpiryDate);


                return View("ConfirmBooking"/*, authMessageSender.SendEmailAsync("attila.starkenius@yh.nackademin.se", "Hey Attila!", "How are you?")*/);
            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }



                
        public IActionResult ShowSeatDetails(int Id)
        {

            try
            {

                BookViewModel model = new BookViewModel();

                model.flight.FlightId = Id;

                TravelAgencyContext context = new TravelAgencyContext();

                model.seatsInFlight = context.Flight
               .Where(c => c.FlightId == model.FlightId)
               .GroupBy(h => h.SeatId)
               .Select(g => new SelectListItem
               {
                   Value = g.Key.ToString(),
                   Text = g.First().SeatId.ToString()
               });

                return View("Index", model);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }



        public IActionResult ViewBookings(string Email, SearchIndexViewModel searchModel)
        {

            try
            {

                var db = new TravelAgencyContext();


                //searchModel.reservationDetailItems = (from c in db.ReservationDetails
                //                                      orderby c.ReservationDetailsId
                //                                      where c.Email.Equals(HttpContext.User.Identity.Name)
                //                                      select c);

                searchModel.reservationDetailsItems = (from c in db.ReservationDetails
                                                      orderby c.ReservationDetailsId
                                                      where c.Email.Equals(HttpContext.User.Identity.Name)
                                                      select c);


                return View("ViewBookings", searchModel);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View("ViewBookings");

            }

        }



        public async Task<IActionResult> Delete(int? id)
        {

            try
            {

                if (id == null)
                {
                    return NotFound();
                }

                var reservationDetail = await _context.ReservationDetails.SingleOrDefaultAsync(m => m.ReservationDetailsId == id);
                if (reservationDetail == null)
                {
                    return NotFound();
                }

                return View(reservationDetail);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }


        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            try
            {

                var reservationDetail = await _context.ReservationDetails.SingleOrDefaultAsync(m => m.ReservationDetailsId == id);
                _context.ReservationDetails.Remove(reservationDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction("ViewBookings");

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }


        
        public async Task<IActionResult> Edit(int? id)
        {

            try
            {

                if (id == null)
                {
                    return NotFound();
                }

                var reservationDetail = await _context.ReservationDetails.SingleOrDefaultAsync(m => m.ReservationDetailsId == id);
                if (reservationDetail == null)
                {
                    return NotFound();
                }
                return View(reservationDetail);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationDetailsId,ReservationId,BookingDate,Insurance,DepartureTime,ArrivalTime,Price,FirstClass,SeatId,InvoiceExpiryDate,RoomId,Breakfast,Email")] ReservationDetails reservationDetails)
        {

            try
            {

                if (id != reservationDetails.ReservationDetailsId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {

                        BookViewModel model = new BookViewModel();

                        if(reservationDetails.RoomId.HasValue) { 
                        model.RoomId = reservationDetails.RoomId.Value;
                            var numberOfDays = reservationDetails.ArrivalTime.Value.Subtract(reservationDetails.DepartureTime.Value);


                            if (numberOfDays.TotalDays < 1)
                            {

                                return View("errorWrongDate", model);

                            }


                            model.hotel = (from c in _context.Hotel
                                           where c.RoomId.Equals(model.RoomId)
                                           orderby c.RoomId
                                           select c).First();

                            reservationDetails.Price = numberOfDays.TotalDays * model.hotel.Price;
                        }

                        if (reservationDetails.SeatId.HasValue)
                        {

                            model.flight = (from c in _context.Flight
                                            where c.SeatId.Equals(reservationDetails.SeatId)
                                            orderby c.SeatId
                                            select c).First();

                            reservationDetails.Price = model.flight.FlightPrice;
                            reservationDetails.DepartureTime = model.flight.DepartureTime;
                            reservationDetails.ArrivalTime = model.flight.ArrivalTime;

                            //model.SeatId = reservationDetails.SeatId.Value;

                        }

                        if (reservationDetails.DepartureTime.HasValue)
                        {
                            model.DepartureTime = reservationDetails.DepartureTime.Value;
                        }

                        if (reservationDetails.ArrivalTime.HasValue)
                        {
                            model.ArrivalTime = reservationDetails.ArrivalTime.Value;
                        }




                        //var numberOfDays = reservationDetails.ArrivalTime.Value.Subtract(reservationDetails.DepartureTime.Value);


                        //if (numberOfDays.TotalDays < 1)
                        //{

                        //    return View("errorWrongDate", model);

                        //}


                        //model.hotel = (from c in _context.Hotel
                        //               where c.RoomId.Equals(model.RoomId)
                        //               orderby c.RoomId
                        //               select c).First();

                        //reservationDetails.Price = numberOfDays.TotalDays * model.hotel.Price;


                        _context.Update(reservationDetails);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ReservationExists(reservationDetails.ReservationDetailsId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("ViewBookings");
                }
                return View(reservationDetails);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }


        private bool ReservationExists(int id)
        {

            try
            {

                return _context.ReservationDetails.Any(e => e.ReservationDetailsId == id);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return false;

            }

        }


    }
}