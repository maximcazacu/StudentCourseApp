using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace StudentCourseApp.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                await context.Database.MigrateAsync();

                // Creăm rolul Admin dacă nu există
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                // Creăm un utilizator Admin
                string adminEmail = "admin@localhost";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                    await userManager.CreateAsync(adminUser, "Admin!123");
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }

                // Aici poți adăuga și seed pentru alți utilizatori sau roluri
            }
        }
    }
}
