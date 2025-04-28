using Microsoft.AspNetCore.Mvc;

namespace SeniorActivitySupportSystem.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
