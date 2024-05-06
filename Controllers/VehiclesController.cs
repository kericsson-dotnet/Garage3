using Garage.Data;
using Garage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Garage.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IRepository<Vehicle> _repository;

        public VehiclesController(IRepository<Vehicle> repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var vehicles = await _repository.GetAll();
            return View(vehicles);
        }

        public async Task<IActionResult> Details(int id)
        {
            var vehicle = await _repository.Get(id);
            return vehicle == null ? NotFound() : View(vehicle);
        }

        public IActionResult Create()
        {
            return View();
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

        [HttpPost, ActionName("Edit")]
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
            var vehicle = await _repository.Get(id);
            if(vehicle == null)
            {
                return NotFound();
            }
            await _repository.Delete(vehicle);
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
