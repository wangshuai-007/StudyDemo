using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MvcCookieAuthSample.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser,ApplicationUserRole,int>
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }
    }
}
