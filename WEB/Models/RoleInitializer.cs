using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Models
{
    /// <summary>
    /// Класс для создания роли admin и пользователя admin.
    /// </summary>
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "Qq12345!";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                IdentityUser admin = new() { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
            string testuser = "testuser@gmail.com";
            string testpassword = "Qq12345!";
            if (await userManager.FindByNameAsync("testuser") == null)
            {
                IdentityUser testUser = new() { Email = testuser, UserName = testuser, EmailConfirmed = true };
                await userManager.CreateAsync(testUser, testpassword);
            }
        }
    }
}