using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeniorActivitySupportSystem.Data;
using SeniorActivitySupportSystem.Interfaces;
using SeniorActivitySupportSystem.Models;
using SeniorActivitySupportSystem.Services;
using SeniorActivitySupportSystem.ViewModel;

namespace SeniorActivitySupportSystem.Controllers
{
    public class SportGroupController : Controller
    {

        private readonly ISportGroupRepository _sportGroupRepository;
        private readonly ICloudinaryService _cloudinary;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SportGroupController(ISportGroupRepository sportGroupRepository, ICloudinaryService cloudinary, IHttpContextAccessor httpContextAccessor)
        {

            _sportGroupRepository = sportGroupRepository;
            _cloudinary = cloudinary;
            _httpContextAccessor = httpContextAccessor;
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
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createSportGroupViewModel = new CreateSportGroupViewModel { AppUserId = curUserId };
            return View(createSportGroupViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateSportGroupViewModel sportGroupVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _cloudinary.AddPhotoAsync(sportGroupVM.Image);

                if (result.Error != null)
                {
                    ModelState.AddModelError("", "Image upload error: " + result.Error.Message);
                    return View(sportGroupVM);
                }

                var sportGroup = new SportGroup
                {
                    Name = sportGroupVM.Name,
                    Description = sportGroupVM.Description,
                    Image = result.SecureUrl.ToString(), 
                    AppUserId = sportGroupVM.AppUserId,
                    Address = new Address
                    {
                        City = sportGroupVM.Address.City,
                        Street = sportGroupVM.Address.Street,
                        PostalCode = sportGroupVM.Address.PostalCode
                    }
                };
                _sportGroupRepository.Add(sportGroup);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Model validation failed");
            }
            return View(sportGroupVM);
        }
        public async Task<IActionResult>Edit (int id)
        {
            var sportGroup = await _sportGroupRepository.GetByIdAsync(id);
            if (sportGroup == null) return View("Error");
            var sportGroupVM = new EditSportGroupViewModel
            {
                Name = sportGroup.Name,
                Description = sportGroup.Description,
                AddressId = sportGroup.AddressId,
                Address = sportGroup.Address,
                AppUserId = sportGroup.AppUserId,
                URL = sportGroup.Image,
                SportGroupCategory = sportGroup.SportGroupCategory,
            };
            return View(sportGroupVM);
        }

        [HttpPost]
        public async Task<IActionResult>Edit(int id, EditSportGroupViewModel sportGroupVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit");
                return View("Edit", sportGroupVM);
            }
            var userSportGroup = await _sportGroupRepository.GetByIdAsyncNoTracking(id);

            if (userSportGroup != null)
            {
                try
                {
                    await _cloudinary.DeletePhotoAsync(userSportGroup.Image);

                }
                catch
                {
                    ModelState.AddModelError("", "Could not delete image");
                    return View("Edit", sportGroupVM);
                }
                var imageResult = await _cloudinary.AddPhotoAsync(sportGroupVM.Image);

                var sportGroup = new SportGroup
                {
                    Id = id,
                    Name = sportGroupVM.Name,
                    Description = sportGroupVM.Description,
                    Image = imageResult.Url.ToString(),
                    AppUserId = sportGroupVM.AppUserId,
                    AddressId = sportGroupVM.AddressId,
                    Address = sportGroupVM.Address
                };

                _sportGroupRepository.Update(sportGroup);
                return RedirectToAction("Index");
            }
            else
            {
                return View(sportGroupVM);
            }
           
            

        }
    }
}
