using Garage.Models;
using Garage.Data;
using Garage.Services;
using Microsoft.AspNetCore.Mvc;

namespace Garage.Controllers
{
    public class UserController : Controller
    {
        private readonly IGarageService _garageService;
        private readonly IRepository<User> _repository;

        public UserController(IRepository<User> repository, IGarageService garageService)
        {
            _garageService = garageService;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _repository.GetAll();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                await _repository.Add(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Vehicles(int id)
        {
            var parkedVehicleIds = new List<int>();
            var user = await _repository.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            foreach (var vehicle in user.Vehicles)
            {
                if (await _garageService.IsVehicleParkedAsync(vehicle.VehicleId))
                {
                    parkedVehicleIds.Add(vehicle.VehicleId);
                }
            }
            ViewBag.ParkedVehicleIds = parkedVehicleIds ?? new List<int>();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> ParkVehicle(int vehicleId)
        {

            var parkingEvent = await _garageService.ParkVehicleAsync(vehicleId);

            var user = parkingEvent.Vehicle.Owner;
            return RedirectToAction("Vehicles", new {id = user.UserId});
        }
        [HttpPost]
        public async Task<IActionResult> UnParkVehicle(int vehicleId)
        {
            var parkingEvent = await _garageService.UnParkVehicleAsync(vehicleId);
            var user = parkingEvent.Vehicle.Owner;
            return RedirectToAction("Vehicles", new {id = user.UserId});
        }
    }
}