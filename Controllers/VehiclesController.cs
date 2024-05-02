using Garage.Data;
using Garage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Garage.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly GarageDbContext _dbContext;

        public VehiclesController(GarageDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {

            return View(await _dbContext.Vehicles.ToListAsync());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle =_dbContext.Vehicles
                //.Include(v => v.User)
                .FirstOrDefault(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,RegNumber,Brand,Model,Color,NumberOfWheels")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(vehicle);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = _dbContext.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,RegNumber,Brand,Model,Color,NumberOfWheels")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(vehicle);
                    _dbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
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

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = _dbContext.Vehicles
               //.Include(v => v.User)
                .FirstOrDefault(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var vehicle = _dbContext.Vehicles.Find(id);
            _dbContext.Vehicles.Remove(vehicle);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        private bool VehicleExists(int id)
        {
            return _dbContext.Vehicles.Any(e => e.Id == id);
        }

    }

}
