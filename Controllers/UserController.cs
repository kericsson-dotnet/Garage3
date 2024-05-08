using Garage.Models;
using Garage.Data;
using Garage.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            var users = await _repository.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.FirstName.Contains(searchString)
                || u.LastName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(u => u.FirstName.Substring
                    (0, Math.Min(2, u.FirstName.Length)))
                        .ThenBy(u => u.FirstName)
                        .ThenBy(u => u.LastName)
                        .ToList();
                    break;
                default:
                    users = users.OrderBy(u => u.FirstName.Substring
                    (0, Math.Min(2, u.FirstName.Length)))
                        .ThenBy(u => u.FirstName)
                        .ThenBy(u => u.LastName)
                        .ToList();
                    break;
            }
            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _repository.Get(id);
            return user is null ? NotFound() : View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create(User user)
        {
            try
            {
                await _repository.Add(user);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists contect system administrator.");
            }

            return View(user);
        }

        public async Task<IActionResult> Edit(int id)
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

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }


            try
            {
                await _repository.Update(user);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists contect system administrator.");
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ParkVehicle(int vehicleId)
        {

            var parkingEvent = await _garageService.ParkVehicleAsync(vehicleId);

            var user = parkingEvent.Vehicle.Owner;
            return RedirectToAction("Vehicles", new { id = user.UserId });
        }
        [HttpPost]
        public async Task<IActionResult> UnParkVehicle(int vehicleId)
        {
            var parkingEvent = await _garageService.UnParkVehicleAsync(vehicleId);
            var user = parkingEvent.Vehicle.Owner;
            return RedirectToAction("Vehicles", new { id = user.UserId });
        }
    }
}