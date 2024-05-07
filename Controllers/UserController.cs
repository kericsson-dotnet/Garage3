using Garage.Models;
using Garage.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Garage.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository<User> _repository;

        public UserController(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var users = await _repository.GetAll();

            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(u => u.FirstName.Substring
                    (0, Math.Min(2, u.FirstName.Length)))
                        .ThenBy(u => u.FirstName)
                        .ThenBy(u => u.LastName)
                        .ToList();
                    break;
                default:
                    users = users.OrderBy(u => u.FirstName.Substring
                    (0, Math.Min(2, u.FirstName.Length)))
                        .ThenBy(u => u.FirstName)
                        .ThenBy(u => u.LastName)
                        .ToList();
                    break;
            }
            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var user = await _repository.Get(Int16.Parse(id));

            return user is not null ? View(user) : NotFound();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repository.Add(user);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists contect system administrator.");
            }

            return View(user);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _repository.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(user);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists contect system administrator.");
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _repository.Get(id);
            return (user is null) ? NotFound() : View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            User user = await _repository.Get(id);
            if(user == null)
            {
                return NotFound();
            }
            await _repository.Delete(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
