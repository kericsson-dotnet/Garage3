using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Garage.Services;
using Garage.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Garage.Controllers
{
    public class SeedController : Controller
    {
        private readonly ISeedingService _seedingService;

        public SeedController(ISeedingService seedingService)
        {
            _seedingService = seedingService;
        }

        [HttpPost]
        public async Task<IActionResult> SeedDatabase()
        {
            // Seed in the various entities in the correct order (add users todo)
            var vehicleTypeSeeds = LoadVehicleTypeSeedsToList();
            await _seedingService.AddVehicleTypeSeedsAsync(vehicleTypeSeeds);

            var vehicleSeeds = LoadVehicleSeedsToList();
            await _seedingService.AddVehicleSeedsAsync(vehicleSeeds);

            return RedirectToAction("Index", "Home");
        }

        private List<VehicleType> LoadVehicleTypeSeedsToList()
        {
            var relativePath = Path.Combine("Data", "MockupData", "SeedVehicleTypes.txt");
            var fullPath = Path.GetFullPath(relativePath);
            var json = System.IO.File.ReadAllText(fullPath);
            var vehicleTypeSeeds = JsonSerializer.Deserialize<List<VehicleType>>(json);
            return vehicleTypeSeeds!;
        }

        private List<Vehicle> LoadVehicleSeedsToList()
        {
            var relativePath = Path.Combine("Data", "MockupData", "SeedVehicles.txt");
            var fullPath = Path.GetFullPath(relativePath);
            var json = System.IO.File.ReadAllText(fullPath);
            var vehicleSeeds = JsonSerializer.Deserialize<List<Vehicle>>(json);
            return vehicleSeeds!;
        }
    }
}
