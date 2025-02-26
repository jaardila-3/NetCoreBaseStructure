using DotNetEnv;
using Microsoft.AspNetCore.Localization;
using Serilog;
using System.Globalization;
using WebApp.Data.Extensions;
using WebApp.Web.DependencyInjection;
using WebApp.Web.Middlewares;

// Load environment variables from .env file
Env.Load();

// Configure culture info for the application
var cultureInfo = new CultureInfo("es-CO");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Configure localization options
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(cultureInfo),
    SupportedCultures = new List<CultureInfo> { cultureInfo },
    SupportedUICultures = new List<CultureInfo> { cultureInfo }
};

var builder = WebApplication.CreateBuilder(args);

// Add configuration session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// configure the application routes to use lowercase urls
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Configure serilog
builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

// Add services registration
builder.Services.AddApplicationServices(builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// Configure session (must be before routing and middleware that uses it)
app.UseSession();

// Configure the middleware to handle global exceptions
app.UseMiddleware<GlobalExceptionMiddleware>();

// Execute database initialization and seed data
await app.Services.InitializeDatabaseAsync();

// Configure localization
app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
