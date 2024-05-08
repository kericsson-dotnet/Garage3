using Garage.Data;
using Garage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Garage.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IRepository<Vehicle> _repository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<VehicleType> _vehicleTypeRepository;


        public VehiclesController(IRepository<Vehicle> repository, IRepository<User> userRepository, IRepository<VehicleType> vehicleTypeRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
        }
        public async Task<IActionResult> Index()
        {
            var vehicle = await _repository.GetAll();
            return vehicle == null ? NotFound() : View(vehicle);
        }


        public async Task<IActionResult> Details(int id)
        {
            var vehicle = await _repository.Get(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        public async Task<IActionResult> Create()
        {
            var users = await _userRepository.GetAll();
            var vehicleTypes = await _vehicleTypeRepository.GetAll();

            ViewBag.UserIds = new SelectList(users.Select(u => new { u.UserId, FullName = u.FirstName + " " + u.LastName }), "UserId", "FullName");
            ViewBag.VehicleTypeIds = new SelectList(vehicleTypes, "VehicleTypeId", "TypeName");

            return View(new Vehicle());
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repository.Add(vehicle);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists contect system administrator.");
            }

            var users = await _userRepository.GetAll();
            ViewBag.UserIds = new SelectList(users, "UserId", "LastName");
            var vehicleTypes = await _vehicleTypeRepository.GetAll();
            ViewBag.VehicleTypeIds = new SelectList(vehicleTypes, "VehicleTypeId", "TypeName");

            return View(vehicle);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var vehicle = await _repository.Get(id);
            return vehicle is null ? NotFound() : View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vehicle vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(vehicle);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.VehicleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists contect system administrator.");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        private bool VehicleExists(int id)
        {
            return _repository.Get(id) is not null;
        }


    }
}
