using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelWebApp.Models;

namespace TravelWebApp.Services
{
    public static class SeedHelpercs
    {
        public static async Task Seed(IServiceProvider provider)
        {
            var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                UserManager<IdentityUser> userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<IdentityUser>>();

                RoleManager<IdentityRole> roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

                var profileDataService = scope.ServiceProvider.GetService<IDataService<Profile>>();

                // Add new role Admin.
                var role = await roleManager.FindByNameAsync("Admin");
                if (role == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                // Add new role Customer.
                role = await roleManager.FindByNameAsync("Customer");
                if (role == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("Customer"));
                }

                // Add new role Customer.
                role = await roleManager.FindByNameAsync("Provider");
                if (role == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("Provider"));
                }

                // Add default user in Admin role.
                var user = await userManager.FindByNameAsync("Luis");
                if (user == null)
                {
                    IdentityUser tempUser = new IdentityUser("Luis");
                    tempUser.Email = "luis_dvalle@hotmail.com";
                    await userManager.CreateAsync(tempUser, "AmIgOs20!");
                    await userManager.AddToRoleAsync(tempUser, "Admin");

                    // Add default Profile.
                    Profile profile = new Profile
                    {
                        FirstName = "Enter your first name",
                        LastName = "Enter your last name",
                        UserId = tempUser.Id
                    };
                    profileDataService.Create(profile);
                }

                // Add default user in Customer role.
                user = await userManager.FindByNameAsync("Alberto");
                if (user == null)
                {
                    IdentityUser tempUser = new IdentityUser("Alberto");
                    tempUser.Email = "alberto@email.com";
                    await userManager.CreateAsync(tempUser, "AmIgOs20!");
                    await userManager.AddToRoleAsync(tempUser, "Customer");

                    // Add default Profile.
                    Profile profile = new Profile
                    {
                        FirstName = "Enter your first name",
                        LastName = "Enter your last name",
                        UserId = tempUser.Id
                    };
                    profileDataService.Create(profile);
                }

                // Add default user in Provider role.
                user = await userManager.FindByNameAsync("Flightcentre");
                if (user == null)
                {
                    IdentityUser tempUser = new IdentityUser("Flightcentre");
                    tempUser.Email = "flight.centre@email.com";
                    await userManager.CreateAsync(tempUser, "AmIgOs20!");
                    await userManager.AddToRoleAsync(tempUser, "Provider");

                    // Add default Profile.
                    Profile profile = new Profile
                    {
                        FirstName = "Enter your first name",
                        LastName = "Enter your last name",
                        UserId = tempUser.Id
                    };
                    profileDataService.Create(profile);
                }
            }
        }
    }
}
