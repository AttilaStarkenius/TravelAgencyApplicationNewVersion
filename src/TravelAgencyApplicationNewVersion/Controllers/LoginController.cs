using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using TravelAgencyApplicationNewVersion.Models;
using TravelAgencyApplicationNewVersion.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TravelAgencyApplicationNewVersion.Data;
using Microsoft.AspNetCore.Http;
using TravelAgencyApplicationNewVersion.ViewModels;

namespace TravelAgencyApplicationNewVersion.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {

        private readonly ApplicationDbContext _context;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;

        public LoginController(

            ApplicationDbContext context,

            RoleManager<IdentityRole> roleManager,

            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender
            )
        {

            _context = context;

            _roleManager = roleManager;

            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
        }





        //
        // GET: /Login/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {

            try
            {

                ViewData["ReturnUrl"] = returnUrl;
                return View();

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }


        //
        // POST: /Login/LoginHotel
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginHotel(LogInViewModel model, string returnUrl = null)
        {

            try
            {

                TempData["ChosenHotel"] = model.hotel.HotelId;


               



                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToLocalHotel(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(model);
                    }
                }

                return View(model);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }



        //
        // POST: /Login/LoginWithoutBooking
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWithoutBooking(LogInViewModel model, string returnUrl = null)
        {

            try
            {
               
                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToLocalWithoutBooking(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(model);
                    }
                }

                return View(model);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }



        //
        // POST: /Login/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogInViewModel model, string returnUrl = null)
        {

            try
            {

                

                TempData["ChosenFlight"] = model.flight.FlightId;


                TempData["ChosenReturnFlight"] = model.ReturnFlightID;


                TempData["FlightPrice"] = model.flight.FlightPrice;


                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {                    
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(model);
                    }
                }

                return View(model);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }



        [AllowAnonymous]
        private IActionResult RedirectToLocalWithoutBooking(string returnUrl)
        {

            try
            {

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(SearchController.Index), "Search");
                }

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }



        [AllowAnonymous]
        private IActionResult RedirectToLocalHotel(string returnUrl)
        {

            try
            {

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(BookController.BookRoom), "Book");
                }

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }



        [AllowAnonymous]
        private IActionResult RedirectToLocal(string returnUrl)
        {

            try
            {

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(BookController.Index), "Book");
                }

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }


        //
        // POST: /Login/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            try { 

            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SearchController.Index), "Search");

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }


        [AllowAnonymous]
        public IActionResult Index()
        {

            try
            {

                return View("~/Views/Login/Login.cshtml");

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);               

                return View("Search");

            }

        }
    }
}