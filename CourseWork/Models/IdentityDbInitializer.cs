using System.Threading.Tasks;
using CourseWork.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace CourseWork.Models
{
    public static class IdentityDbInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            const string email = "admin@admin.com";
            const string password = "Admin?1";
            var admin = new User
            {
                Email = email,
                UserName = email
            };

            if (await roleManager.FindByNameAsync(Role.Admin) == null)
                await roleManager.CreateAsync(new IdentityRole(Role.Admin));
            if (await roleManager.FindByNameAsync(Role.User) == null)
                await roleManager.CreateAsync(new IdentityRole(Role.User));

            if (await userManager.FindByNameAsync(email) == null)
            {
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded) await userManager.AddToRolesAsync(admin, new[] {Role.User, Role.Admin});
            }
        }
    }
}