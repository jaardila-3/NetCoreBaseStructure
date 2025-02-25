using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Web.Models;

namespace WebApp.Web.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        throw new InvalidOperationException("This is a test exception");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var errorModel = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            ErrorStatusCode = HttpContext.Session?.GetInt32("ErrorStatusCode"),
            ErrorMessage = HttpContext.Session?.GetString("ErrorMessage")
        };
        HttpContext.Session?.Remove("ErrorMessage");
        HttpContext.Session?.Remove("ErrorStatusCode");
        return View(errorModel);
    }
}
