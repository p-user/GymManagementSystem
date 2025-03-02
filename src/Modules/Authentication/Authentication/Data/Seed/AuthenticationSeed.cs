using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shared.Constants;
using Shared.Data;

namespace Authentication.Data.Seed
{
    public class AuthenticationSeed(IServiceProvider serviceProvider) : ISeed
    {
        public async Task SeedAsync()
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Models.Role>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Models.User>>();

            string[] roleNames = { DefaultRoles.AdminRole, DefaultRoles.MemberRole, DefaultRoles.TrainerRole, DefaultRoles.RecepsionistRole };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    var newRole = new Models.Role()
                    {
                        Name = roleName,

                    };
                    roleResult = await roleManager.CreateAsync(newRole);
                }
            }

            var admin = new Models.User
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true,
            };

            string adminPassword = "Admin123!";
            var _user = await userManager.FindByEmailAsync("admin@admin.com");

            if (_user == null)
            {
                var createAdmin = await userManager.CreateAsync(admin, adminPassword);
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, DefaultRoles.AdminRole);
                }
            }
        }
    }
}
