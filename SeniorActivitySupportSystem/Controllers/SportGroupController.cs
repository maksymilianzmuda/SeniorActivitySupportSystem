using Microsoft.AspNetCore.Mvc;

namespace SeniorActivitySupportSystem.Controllers
{
    public class SportGroupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
