using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Garage.Models;
using Garage.Services;

namespace Garage.Controllers;

public class HomeController : Controller
{
    private readonly IGarageService _garageService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IGarageService garageService)
    {
        _logger = logger;
        _garageService = garageService;
    }

    public async Task<IActionResult> Index()
    {
        var garageStatus = await _garageService.GetGarageStatusAsync();
        return View(garageStatus);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
