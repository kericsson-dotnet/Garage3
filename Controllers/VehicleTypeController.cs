using Garage.Data;
using Garage.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Garage.Controllers
{
    public class VehicleTypeController : Controller
    {
        private readonly IRepository<VehicleType> _repository;

        public VehicleTypeController(IRepository<VehicleType> repository)
        {
            _repository = repository;
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
            var relativePath = Path.Combine("Data", "MockupData", "SeedVehicleTypess.txt");
            var fullPath = Path.GetFullPath(relativePath);
            var json = System.IO.File.ReadAllText(fullPath);
            var vehicleTypeSeeds = JsonConvert.DeserializeObject<List<VehicleType>>(json);
            return vehicleTypeSeeds!;
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicleTypeSeedsToDb()
        {
            var vehicleTypeSeeds = LoadVehicleTypeSeedsToList();

            foreach (var vehicleType in vehicleTypeSeeds)
            {
                await _repository.Add(vehicleType);
            }
            return RedirectToAction(nameof(Index));
        }
        // Implement other actions (Edit, Details, Delete)...
    }
}