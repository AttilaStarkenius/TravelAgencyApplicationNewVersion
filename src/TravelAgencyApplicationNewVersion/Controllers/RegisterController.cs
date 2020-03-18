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
using TravelAgencyApplicationNewVersion.ViewModels;

namespace TravelAgencyApplicationNewVersion.Controllers
{
    [Authorize]
    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;

        public RegisterController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<RegisterController>();
        }

        //
        // GET: /Register/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
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
        // POST: /Register/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVIewModel model, string returnUrl = null)
        {

            try
            {

                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        Member = model.Member,
                        Name = model.Name,
                        PhoneNumber = model.PhoneNumber,
                        Address = model.Address
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation(3, "User created a new account with password.");
                        return RedirectToLocal(returnUrl);
                    }
                    AddErrors(result);
                }

                return View(model);

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }

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
                    return RedirectToAction(nameof(SearchController.Index), "Search");
                }

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View();

            }

        }

        private void AddErrors(IdentityResult result)
        {

            try
            {

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return;

            }

        }

        public IActionResult Index()
        {

            try { 

            return View("~/Views/Register/Register.cshtml");

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);

                return View("Search");

            }

        }
    }
}