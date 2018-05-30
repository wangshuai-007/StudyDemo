using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace MvcCookieAuthSample.Data
{
    public class ApplicationDbContextSeed
    {
        private UserManager<ApplicationUser> _userManager;

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Users.Any())
            {
                _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var defaultUser=new ApplicationUser()
                {
                    UserName = "Administrator",
                    Email = "w371987114@qq.com",
                    NormalizedUserName = "admin"
                };
                var result = await _userManager.CreateAsync(defaultUser, "Passwoerd$123");
                if (!result.Succeeded)
                {
                    throw new Exception("初始用户失败！");
                }
            }
        }
    }
}
