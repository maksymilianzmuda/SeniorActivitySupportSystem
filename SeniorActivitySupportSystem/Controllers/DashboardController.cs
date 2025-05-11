using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using SeniorActivitySupportSystem.Data;
using SeniorActivitySupportSystem.Interfaces;
using SeniorActivitySupportSystem.Models;
using SeniorActivitySupportSystem.ViewModel;

namespace SeniorActivitySupportSystem.Controllers
{
    public class DashboardController : Controller
    {

        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICloudinaryService _cloudinaryService;
        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor, ICloudinaryService cloudinaryService)
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor= httpContextAccessor;
            _cloudinaryService= cloudinaryService;
        }
        private void MapUserEdit(AppUser user, EditUserDashboardViewModel editVM, ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.FirstName = editVM.FirstName;
            user.LastName = editVM.LastName;
            user.Bio = editVM.Bio;
            user.Gender = editVM.Gender;
            user.DateOfBirth = editVM.DateOfBirth;
            user.ProfileImageUrl = photoResult.Url.ToString();
            user.City = editVM.City;
            user.Street = editVM.Street;
            user.PostalCode = editVM.PostalCode;
        }
        public async Task<IActionResult> Index(string? id)
        {

            if (string.IsNullOrEmpty(id))
            {
                id = _httpContextAccessor.HttpContext.User.GetUserId();
                if (id == null) return RedirectToAction("Login", "Account");
            }

            var user = await _dashboardRepository.GetUserById(id);
            if (user == null) return View("Error");

            var userEvents = await _dashboardRepository.GetAllEvents();
            var userGroups = await _dashboardRepository.GetAllGroups();

            var dashboardViewModel = new DashboardViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Bio = user.Bio,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                ProfileImageUrl = user.ProfileImageUrl,
                Street = user.Street,
                City = user.City,
                PostalCode = user.PostalCode,
                SportEvents = userEvents,
                SportGroups = userGroups
            };
            return View(dashboardViewModel);
        }

        public async Task<IActionResult> EditProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(curUserId);
            if (user == null) return View("Error");
            var editUserDashboardVM = new EditUserDashboardViewModel()
            {
                Id = curUserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Bio = user.Bio,
                ProfileImageUrl = user.ProfileImageUrl,
                Gender = user.Gender,
                City = user.City,
                Street= user.Street,
                PostalCode= user.PostalCode

            };
            return View(editUserDashboardVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditUserDashboardViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to save the changes");
                return View("EditProfile", editVM);
            }
            AppUser user = await _dashboardRepository.GetByIdNoTracking(editVM.Id);

            if(user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _cloudinaryService.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);
                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _cloudinaryService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Error occured while deleting the photo");
                }
                var photoResult = await _cloudinaryService.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);
                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            }
        }
    }
}
