using Garage.Data;
using Garage.Models;
using Microsoft.AspNetCore.Mvc;

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
            var vehicletypes = await _repository.GetAll();
            return View(vehicletypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleType vehicletype)
        {
            if (ModelState.IsValid)
            {
                await _repository.Add(vehicletype);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicletype);
        }

        // Implement other actions (Edit, Details, Delete)...
    }
}