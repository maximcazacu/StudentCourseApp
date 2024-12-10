using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using StudentCourseApp.Data;
using StudentCourseApp.Hubs;
using StudentCourseApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Adăugăm serviciile MVC și configurăm JSON pentru a rezolva ciclurile
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

// Configurare DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adăugare Identity cu UI default (paginile de /Identity/Account/Login etc.)
// AddDefaultIdentity adaugă IdentityUser și UI implicit de la Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

// Adăugăm SignalR
builder.Services.AddSignalR();

var app = builder.Build();

// Inițializare date (dacă ai DbInitializer)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.InitializeAsync(services);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Middleware Identity
app.UseAuthentication();
app.UseAuthorization();

// Maparea rutelor pentru Razor Pages ale Identity
app.MapRazorPages(); // Foarte important pentru /Identity/Account/Login etc.
// Maparea rutelor MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Maparea hub-ului SignalR
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
