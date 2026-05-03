using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence
{
    public class IdentitySeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Rol ekle
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            // Admin user ekle
            if (await userManager.FindByEmailAsync("admin@test.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "Admin123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }

                var user1 = new ApplicationUser { UserName = "user1", Email = "user1@test.com", EmailConfirmed = true };
                var resultUser1 = await userManager.CreateAsync(user1, "User123!");
                if (resultUser1.Succeeded) { await userManager.AddToRoleAsync(user1, "User"); }

                var user2 = new ApplicationUser { UserName = "user2", Email = "user2@test.com", EmailConfirmed = true };
                var resultUser2 = await userManager.CreateAsync(user2, "User123!");
                if (resultUser2.Succeeded) { await userManager.AddToRoleAsync(user2, "User"); }

                var user3 = new ApplicationUser { UserName = "user3", Email = "user3@test.com", EmailConfirmed = true };
                var resultuser3 = await userManager.CreateAsync(user3, "User123!");
                if (resultuser3.Succeeded) { await userManager.AddToRoleAsync(user3, "User"); }
            }
        }
    }
}
