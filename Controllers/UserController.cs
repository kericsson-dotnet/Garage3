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

        public async Task<IActionResult> Index()
        {
            var users = await _repository.GetAll();
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

        public async Task<IActionResult> Vehicles(int id)
        {
            var user = await _repository.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }
}