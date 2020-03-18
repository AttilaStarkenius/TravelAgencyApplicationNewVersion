using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgencyApplicationNewVersion.Data;
using TravelAgencyApplicationNewVersion.Models;

namespace TravelAgencyApplicationNewVersion
{
    public class MyOwnUserStore : UserStore<ApplicationUser>
    {
        public MyOwnUserStore(ApplicationDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {

        }
    }
}
