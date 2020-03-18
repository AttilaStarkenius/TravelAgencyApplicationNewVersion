using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TravelAgencyApplicationNewVersion.Models;
using TravelAgencyApplicationNewVersion.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace TravelAgencyApplicationNewVersion.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministrationController : Controller
    {
        private readonly TravelAgencyContext _context;
        public AdministrationController(TravelAgencyContext context)
        {
            _context = context;
        }
        public IActionResult Index(SearchIndexViewModel searchModel)
        {

            try { 

            var db = new TravelAgencyContext();

                

                searchModel.reservationDetailItems = (from c in db.ReservationDetails
                                                orderby c.ReservationDetailsId
                                                select c);


                return View("AllBookings", searchModel);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View("AllBookings");

            }

        }





        // GET: Administration/Delete/
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



        // POST: Administration/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            try
            {

                var reservationDetail = await _context.ReservationDetails.SingleOrDefaultAsync(m => m.ReservationDetailsId == id);
                _context.ReservationDetails.Remove(reservationDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }



    }
}