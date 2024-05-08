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
        public async Task<IActionResult> Index(string searchString)
        {
            
            var vehicles = await _repository.GetAll();

            // Filter vehicles based on the  search string
            if (!string.IsNullOrEmpty(searchString))
            {
                vehicles = vehicles.Where(v =>
                    v.VehicleType.TypeName.Contains(searchString) || 
                    v.RegNumber.Contains(searchString)); 
            }

            int vehicleCount = vehicles.Count();
            ViewData["VehicleCount"] = vehicleCount;

            return View(vehicles);
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

        [HttpPost]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            var user = await _userRepository.Get(vehicle.UserId);
            var vehicleType = await _vehicleTypeRepository.Get(vehicle.VehicleTypeId);

            vehicle.Owner = user;
            vehicle.VehicleType = vehicleType;

            ModelState.ClearValidationState(nameof(vehicle.Owner));
            TryValidateModel(nameof(vehicle.Owner));
            ModelState.ClearValidationState(nameof(vehicle.VehicleType));
            TryValidateModel(nameof(vehicle.Owner));


            if (ModelState.IsValid)
            {
                await _repository.Add(vehicle);
                return RedirectToAction(nameof(Index));
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
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
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
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await _repository.Get(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            // You can implement your own logic to check if a vehicle exists
            // For simplicity, returning true here assuming all ids are valid
            return true;
        }


    }
}
