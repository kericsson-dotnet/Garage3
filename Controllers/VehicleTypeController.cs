using Garage.Data;
using Garage.Models;
using Garage.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Garage.Controllers
{
    public class VehicleTypeController : Controller
    {
        private readonly IRepository<VehicleType> _repository;
        private readonly ISeedingService _seedingService;

        public VehicleTypeController(IRepository<VehicleType> repository, ISeedingService seedingService)
        {
            _repository = repository;
            _seedingService = seedingService;
        }

        public async Task<IActionResult> Index()
        {
            var vehicleType = await _repository.GetAll();
            return View(vehicleType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleType vehicleType)
        {
            if (ModelState.IsValid)
            {
                await _repository.Add(vehicleType);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleType);
        }

        
        public List<VehicleType> LoadVehicleTypeSeedsToList()
        {
            var relativePath = Path.Combine("Data", "MockupData", "SeedVehicleTypes.txt");
            var fullPath = Path.GetFullPath(relativePath);
            var json = System.IO.File.ReadAllText(fullPath);
            var vehicleTypeSeeds = JsonSerializer.Deserialize<List<VehicleType>>(json);
            return vehicleTypeSeeds!;
        }

        public async Task AddVehicleTypeSeedsToDb()
        {
            var vehicleTypeSeeds = LoadVehicleTypeSeedsToList();
            await _seedingService.AddVehicleTypeSeedsAsync(vehicleTypeSeeds);
        }
    }
}