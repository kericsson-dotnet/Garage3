using Garage.Models;
using Garage.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel;

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
                //int age = CalculateAgeFromPersonalNumber(user.PersonalNumber);
                //if (age >= 18) 
                //{
                   
                //}
                //else
                //{
                //    ModelState.AddModelError("PersonalNumber", "You must be 18 or older to park a vehicle");
                //}
                await _repository.Add(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        //private int CalculateAgeFromPersonalNumber(string personalNumber)
        //{
        //   if(!Regex.IsMatch(personalNumber, @"^\d{6}-\d{4}$"))
        //   {
        //        return -1;
        //   }

        //    var birthDate = personalNumber.Substring(0, 6);
        //    int year = int.Parse(birthDate.Substring(0, 2));
        //    int month = int.Parse(birthDate.Substring(2, 2));
        //    int day = int.Parse(birthDate.Substring(4, 2));

        //    if (year >= 0 && year <= 29)
        //    {
        //        year += 2000;
        //    }
        //    else
        //    {
        //        year += 1900;
        //    }

        //    var birthDateTime = new DateTime(year, month, day);

        //    var today = DateTime.Today;
        //    var age = today.Year - birthDateTime.Year;
        //    if (birthDateTime > today.AddYears(-age)) age--;

        //    return age;
        //}

        // Implement other actions (Edit, Details, Delete)...
    }
}