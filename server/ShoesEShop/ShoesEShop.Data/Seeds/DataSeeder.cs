using Microsoft.AspNetCore.Identity;
using ShoesEShop.Core.Enums;
using ShoesEShop.Data.Entities;

namespace ShoesEShop.Data.Seeds
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        private static async Task SeedUsers(UserManager<AppUser> userManager)
        {
            AppUser admin = new AppUser
            {
                Email = "rum.workspace@outlook.com",
                UserName = "rum.netdev",
                PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "@Admin123"),
                FirstName = "Quang",
                LastName = "Rum",
                PhoneNumber = "84795671811",
                Dob = new DateTime(2001, 11, 18),
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var existing = await userManager.FindByEmailAsync(admin.Email);
            if(existing == null)
            {
                await userManager.CreateAsync(admin);
                // Then give admin role
                if (!await userManager.IsInRoleAsync(admin, DefaultRoleEnum.Admin.ToString()))
                    await userManager.AddToRoleAsync(admin, DefaultRoleEnum.Admin.ToString());
            }
        }

        private static async Task SeedRoles(RoleManager<AppRole> roleManager)
        {
            if(roleManager.Roles.Count() == 0)
            {
                var defaultRoles = Enum.GetNames(typeof(DefaultRoleEnum));
                foreach(var role in defaultRoles)
                {
                    await roleManager.CreateAsync(new AppRole
                    {
                        Name = role,
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    });
                }
            }
        }
    }
}
