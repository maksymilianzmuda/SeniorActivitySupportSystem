using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeniorActivitySupportSystem.Data;
using SeniorActivitySupportSystem.Interfaces;
using SeniorActivitySupportSystem.Models;

namespace SeniorActivitySupportSystem.Controllers
{
    public class SportGroupController : Controller
    {

        private readonly ISportGroupRepository _sportGroupRepository;

        public SportGroupController(ApplicationDbContext context, ISportGroupRepository sportGroupRepository )
        {

            _sportGroupRepository = sportGroupRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<SportGroup> sportGroups = await _sportGroupRepository.GetAll();
            return View(sportGroups);
        }

        public async Task<IActionResult> Detail(int id)
        {
            SportGroup sportGroup = await _sportGroupRepository.GetByIdAsync(id);
            return View(sportGroup);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create (SportGroup sportGroup)
        {
            if (ModelState.IsValid)
            {
                return View(sportGroup);

            }

            _sportGroupRepository.Add(sportGroup);
            return RedirectToAction("Index");
        }
    }
}
