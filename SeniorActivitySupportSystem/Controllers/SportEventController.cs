using Microsoft.AspNetCore.Mvc;

namespace SeniorActivitySupportSystem.Controllers
{
    public class SportEventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
