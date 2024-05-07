using Garage.Data;
using Garage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Details(int id)
        {
            var vehicleType = await _repository.Get(id);
            return vehicleType is null ? NotFound() : View(vehicleType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create(VehicleType vehicleType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repository.Add(vehicleType);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists contect system administrator.");
            }
            return View(vehicleType);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vehicleType = await _repository.Get(id);
            return vehicleType is null ? NotFound() : View(vehicleType);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleType vehicleType)
        {
            if (id != vehicleType.VehicleTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(vehicleType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists contect system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleType);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var vehicleType = await _repository.Get(id);
            if (vehicleType == null)
            {
                return NotFound();
            }

            return View(vehicleType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleType = await _repository.Get(id);
            if (vehicleType == null)
            {
                return NotFound();
            }
            await _repository.Delete(vehicleType);
            return RedirectToAction(nameof(Index));
        }
    }
}