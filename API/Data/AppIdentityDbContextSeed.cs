using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<UserModel> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new UserModel
                {
                    Email = "example@example.com",
                    UserName = "example@example.com"
                };

                await userManager.CreateAsync(user, "Aa123456!");
            }
        }
    }
}