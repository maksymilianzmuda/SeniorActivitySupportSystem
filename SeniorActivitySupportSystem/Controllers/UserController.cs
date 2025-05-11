using Microsoft.AspNetCore.Mvc;
using SeniorActivitySupportSystem.Interfaces;
using SeniorActivitySupportSystem.ViewModel;
using System.Reflection.Metadata.Ecma335;

namespace SeniorActivitySupportSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("user")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach(var user in users) 
            {
                var userVM = new UserViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    ProfileImageUrl = user.ProfileImageUrl,
                };
                result.Add(userVM);
            }
            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            var userDetailVM = new UserDetailViewModel()
            {
                Id= user.Id,
                Username = user.UserName,
                Gender = user.Gender,
                Bio =   user.Bio,
                DateOfBirth = user.DateOfBirth,
                Address = user.Address,
                ProfileImageUrl = user.ProfileImageUrl,
                SportEvents = user.SportEvents,
                SportGroups = user.SportGroups,
                
            };
            return View(userDetailVM);
        }
    }
}
