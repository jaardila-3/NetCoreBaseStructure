using DotNetEnv;
using Serilog;
using WebApp.Web.DependencyInjection;

// Load environment variables from .env file
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Configure serilog
builder.Host.UseSerilog((context, config) =>
{
    config.MinimumLevel.Error()
          .WriteTo.Console() // Logs in console
          .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day, // Logs file with daily rotation
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}", // Logs with format
            retainedFileCountLimit: 15); // Keep only 15 log files
});

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
