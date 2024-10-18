using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using HolidayHomeProject.Models;
using HolidayHomeProject.DataAccessLayer.Context;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DbConnection") ?? throw new InvalidOperationException("Connection string 'DbConnection' not found.");

var services = builder.Services;

// Add a database provider 
services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\synth\\OneDrive\\Documents\\HolidayHome.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"));

// Add services to the container.
services.AddControllersWithViews();

// Add Authentication
services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
    });
// Add session services
services.AddSession();

services.AddRazorPages();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


// Enable session
app.UseSession();


app.UseEndpoints(endpoints =>
{
	endpoints.MapRazorPages();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
