using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.SqlServer;
using WeatherApplication.BusinessLogic.Abstractions;
using WeatherApplication.BusinessLogic.Implementations;
using WeatherApplication.Data;
using WeatherApplication.DataAccess.Abstractions;
using WeatherApplication.DataAccess.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    //.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

//IConfiguration config = builder.Build();

// Add services to the container.
//builder.Services.AddDbContext<WeatherDbContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<WeatherDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddTransient<IUserHistory, UserHistory>();
builder.Services.AddTransient<IUserHistoryData, UserHistoryData>();
builder.Services.AddTransient<IWeatherMap, WeatherMap>();
builder.Services.AddTransient<IWeatherRepository, WeatherRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
