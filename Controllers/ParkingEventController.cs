using Garage.Models;
using Garage.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Details(string id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var parkingEvent = await _repository.Get(Int16.Parse(id));

            return parkingEvent is not null ? View(parkingEvent) : NotFound();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create(ParkingEvent parkingEvent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repository.Add(parkingEvent);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists contect system administrator.");
            }
            return View(parkingEvent);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var parkingEvent = await _repository.Get(id);
            return parkingEvent is null ? NotFound() : View(parkingEvent); ;
        }


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ParkingEvent parkingEvent)
        {
            if (id != parkingEvent.ParkingEventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(parkingEvent);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists contect system administrator.");
                }
            }
            return View(parkingEvent);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var parkingEvent = await _repository.Get(id);
            return parkingEvent is null ? NotFound() : View(parkingEvent);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ParkingEvent parkingEvent = await _repository.Get(id);
            if (parkingEvent == null)
            {
                return NotFound();
            }
            await _repository.Delete(parkingEvent);
            return RedirectToAction(nameof(Index));
        }
    }
}