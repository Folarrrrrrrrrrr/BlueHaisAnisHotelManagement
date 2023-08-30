global using Microsoft.EntityFrameworkCore;
global using BlueHaisAnisHotelManagement.Models;
global using BlueHaisAnisHotelManagement.Data;
global using Microsoft.AspNetCore.Mvc;
global using BlueHaisAnisHotelManagement.Interfaces;
global using BlueHaisAnisHotelManagement.Repositories;
global using Microsoft.AspNetCore.Identity;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRoomUnits, RoomUnitsRepository>();
builder.Services.AddScoped<IRoomProfiles, RoomProfileRepo>();
builder.Services.AddMvcCore();

var connectionString = builder.Configuration.GetConnectionString("BlueHaisAnisConn") ?? throw new InvalidOperationException($"The connection string is not supported.");
builder.Services.AddDbContext<HotelContext>(options => options.UseSqlServer(connectionString));




//builder.Services.AddDbContext<HotelDbContext>(options => options.UseSqlServer(
//    builder.Configuration.GetConnectionString("myconn"))
//   );

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
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//      name: "areas",
//      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    );
//});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
