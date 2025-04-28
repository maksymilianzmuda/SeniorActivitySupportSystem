using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeniorActivitySupportSystem.Data;
using SeniorActivitySupportSystem.Interfaces;
using SeniorActivitySupportSystem.Models;
using SeniorActivitySupportSystem.Repository;

namespace SeniorActivitySupportSystem.Controllers
{
    public class SportEventController : Controller
    {
        private readonly ISportEventRepository _sportEventRepository;

        public SportEventController(ApplicationDbContext context, ISportEventRepository sportEventRepository)
        {
            
            _sportEventRepository = sportEventRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<SportEvent> sportEvents = await _sportEventRepository.GetAll();
            return View(sportEvents);
        }
        public async Task<IActionResult> Detail(int id)
        {
            SportEvent sportEvent = await _sportEventRepository.GetByIdAsync(id);
            return View(sportEvent);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SportEvent sportEvent)
        {
            if (ModelState.IsValid)
            {
                return View(sportEvent);
            }

            _sportEventRepository.Add(sportEvent);
            return RedirectToAction("Index");
        }
    }
}
