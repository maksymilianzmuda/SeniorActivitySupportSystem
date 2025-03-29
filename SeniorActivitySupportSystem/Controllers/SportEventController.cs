using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeniorActivitySupportSystem.Data;
using SeniorActivitySupportSystem.Models;

namespace SeniorActivitySupportSystem.Controllers
{
    public class SportEventController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SportEventController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<SportEvent> sportEvents = _context.SportEvents.ToList();
            return View(sportEvents);
        }
        public IActionResult Detail(int id)
        {
            SportEvent sportEvent = _context.SportEvents.Include(a => a.Address).FirstOrDefault(s => s.Id == id);
            return View(sportEvent);
        }
    }
}
