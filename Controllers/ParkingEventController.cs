using Garage.Models;
using Garage.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Garage.Controllers
{
    public class ParkingEventController : Controller
    {
        private readonly IRepository<ParkingEvent> _repository;

        public ParkingEventController(IRepository<ParkingEvent> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var parkingEvents = await _repository.GetAll();
            return View(parkingEvents);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ParkingEvent parkingEvent)
        {
            if (ModelState.IsValid)
            {
                await _repository.Add(parkingEvent);
                return RedirectToAction(nameof(Index));
            }
            return View(parkingEvent);
        }

        // Implement other actions (Edit, Details, Delete)...
    }
}