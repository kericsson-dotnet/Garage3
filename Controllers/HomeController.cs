using Garage.Models;
using Garage.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Garage.Controllers;

public class HomeController : Controller
{
    private readonly IGarageService _garageService;
    private readonly ILogger<HomeController> _logger;
    private readonly ISeedingService _seedingService;

    public HomeController(ILogger<HomeController> logger, IGarageService garageService, ISeedingService seedingService)
    {
        _logger = logger;
        _garageService = garageService;
        _seedingService = seedingService;
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

    [HttpPost]
    public async Task<IActionResult> SeedDatabase()
    {
        if (!await _seedingService.IsDatabaseSeededAsync())
        {
            await _seedingService.SeedDatabaseAsync();
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Index));
    }

}
