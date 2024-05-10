using Garage.Data;
using Garage.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

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

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var parkingEvents = await _repository.GetAll();
            return View(parkingEvents);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ParkingEvent parkingEvent)
        {
            if (ModelState.IsValid)
            {
                parkingEvent.CheckInTime = DateTime.Now;

                await _repository.Add(parkingEvent);
                return RedirectToAction(nameof(Index));
            }
            return View(parkingEvent);
        }

        [HttpPost]
        public async Task<IActionResult> Unpark(int id)
        {
            var parkingEvent = await _repository.Get(id);
            if (parkingEvent == null)
            {
                return NotFound();
            }

            parkingEvent.CheckOutTime = DateTime.Now;

            await _repository.Update(parkingEvent);

            return RedirectToAction("Index");
        }
    }
}
