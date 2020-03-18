using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgencyApplicationNewVersion.Models;

namespace TravelAgencyApplicationNewVersion.Data
{
    public class AdministratorSeedData
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        public AdministratorSeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task EnsureSeedDataAsync()
        {
            if (await _userManager.FindByEmailAsync("administrator@someone.com") == null)
            {
                ApplicationUser administrator = new ApplicationUser()
                {
                    UserName = "administrator@someone.com",
                    Email = "administrator@someone.com"
                };

                await _userManager.CreateAsync(administrator, "Passw0ärd427!");
                await _roleManager.CreateAsync(new IdentityRole("Administrator"));

                IdentityResult result = await _userManager.AddToRoleAsync(administrator, "Administrator");
            }
        }
    }
}
