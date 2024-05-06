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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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

        // Implement other actions (Edit, Details, Delete)...
    }
}