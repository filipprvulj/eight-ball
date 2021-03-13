using EightBall.Shared.Options;
using EightBall.Shared.Strings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EightBall.Extensions
{
    public static class IdentityDataInitializer
    {
        public static IApplicationBuilder SeedIdentityData(this IApplicationBuilder app, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, EmployeeOptions options)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager, options);
            return app;
        }

        public static void SeedUsers(UserManager<IdentityUser> userManager, EmployeeOptions options)
        {
            if (userManager.FindByNameAsync(options.Email).Result == null)
            {
                IdentityUser identityUser = new IdentityUser
                {
                    Email = options.Email,
                    UserName = options.Email
                };

                IdentityResult result = userManager.CreateAsync(identityUser, options.Password).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(identityUser, RoleNames.Employee).Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(RoleNames.Employee).Result)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = RoleNames.Employee
                };

                roleManager.CreateAsync(identityRole).Wait();
            }

            if (!roleManager.RoleExistsAsync(RoleNames.Visitor).Result)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = RoleNames.Visitor
                };

                roleManager.CreateAsync(identityRole).Wait();
            }
        }
    }
}