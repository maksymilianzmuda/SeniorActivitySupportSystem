using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SeniorActivitySupportSystem.Data;
using SeniorActivitySupportSystem.Models;
using SeniorActivitySupportSystem.ViewModel;

namespace SeniorActivitySupportSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context; 
        }
        public IActionResult Login()
        {
            var response = new LoginViewModel(); 
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult>Login(LoginViewModel loginVM)
        {
            if(!ModelState.IsValid)
            {
                return View(loginVM);
            };
            var user = await _userManager.FindByEmailAsync(loginVM.Email);

            if(user != null)
            {
                var checkPassword = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (checkPassword)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password,false,false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Email or password is incorrect.";
                return View(loginVM);
            }
            TempData["Error"] = "User does not exist";
            return View(loginVM);


        }
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            var user = await _userManager.FindByEmailAsync(registerVM.Email); 
            if(user != null) 
            {
                TempData["Error"] = "This user already exists";
                return View(registerVM);
            }
            var newUser = new AppUser
            {
                UserName = registerVM.Email,  // lub inne pole, jeśli masz
                Email = registerVM.Email,
                DateOfBirth = registerVM.DateOfBirth,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Gender = registerVM.Gender
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser,UserRoles.Admin);
            }
            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
