using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeniorActivitySupportSystem.Data;
using SeniorActivitySupportSystem.Models;

namespace SeniorActivitySupportSystem.Controllers
{
    public class SportGroupController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SportGroupController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<SportGroup> sportGroups = _context.SportGroups.ToList();
            return View(sportGroups);
        }

        public IActionResult Detail(int id)
        {
            SportGroup sportGroup = _context.SportGroups.Include(a => a.Address).FirstOrDefault(s => s.Id == id);
            return View(sportGroup);
        }
    }
}
