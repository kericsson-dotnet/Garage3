using Garage.Models;
using Garage.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Garage.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository<User> _repository;

        public UserController(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] =searchString;
            var users = await _repository.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.FirstName.Contains(searchString)
                || u.LastName.Contains(searchString));
            }
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                await _repository.Add(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // Implement other actions (Edit, Details, Delete)...
    }
}