using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<UserModel> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new  UserModel
                {
                    Email = "team@alphabondsdating.com",
                    UserName = "team@alphabondsdating.com"
                };

                await userManager.CreateAsync(user, "Aa123456!");
            }
        }
    }
}