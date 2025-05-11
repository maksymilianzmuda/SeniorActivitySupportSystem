using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeniorActivitySupportSystem.Data;
using SeniorActivitySupportSystem.Interfaces;
using SeniorActivitySupportSystem.Models;
using SeniorActivitySupportSystem.Repository;
using SeniorActivitySupportSystem.ViewModel;

namespace SeniorActivitySupportSystem.Controllers
{
    public class SportEventController : Controller
    {
        private readonly ICloudinaryService _cloudinary;
        private readonly ISportEventRepository _sportEventRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SportEventController(ISportEventRepository sportEventRepository, ICloudinaryService cloudinary, IHttpContextAccessor httpContextAccessor)
        {
            
            _sportEventRepository = sportEventRepository;
            _cloudinary = cloudinary;
            _httpContextAccessor = httpContextAccessor;
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
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createSportEventViewModel = new CreateSportEventViewModel { AppUserId = curUserId };
            return View(createSportEventViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSportEventViewModel sportEventVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _cloudinary.AddPhotoAsync(sportEventVM.Image);

                if (result.Error != null)
                {
                    ModelState.AddModelError("", "Image upload error: " + result.Error.Message);
                    return View(sportEventVM);
                }

                var sportEvent = new SportEvent
                {
                    Name = sportEventVM.Name,
                    Description = sportEventVM.Description,
                    Image = result.SecureUrl.ToString(), 
                    AppUserId = sportEventVM.AppUserId,
                    Address = new Address
                    {
                        City = sportEventVM.Address.City,
                        Street = sportEventVM.Address.Street,
                        PostalCode = sportEventVM.Address.PostalCode
                    }
                };
                _sportEventRepository.Add(sportEvent);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Model validation failed");
            }
            return View(sportEventVM);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var sportEvent = await _sportEventRepository.GetByIdAsync(id);
            if (sportEvent == null) return View("Error");
            var sportEventVM = new EditSportEventViewModel
            {
                Name = sportEvent.Name,
                Description = sportEvent.Description,
                AddressId = sportEvent.AddressId,
                Address = sportEvent.Address,
                AppUserId = sportEvent.AppUserId,
                URL = sportEvent.Image,
                SportEventCategory = sportEvent.EventCategory,
            };
            return View(sportEventVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditSportEventViewModel sportEventVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit");
                return View("Edit", sportEventVM);
            }
            var userSportGroup = await _sportEventRepository.GetByIdAsyncNoTracking(id);

            if (userSportGroup != null)
            {
                try
                {
                    await _cloudinary.DeletePhotoAsync(userSportGroup.Image);

                }
                catch
                {
                    ModelState.AddModelError("", "Could not delete image");
                    return View("Edit", sportEventVM);
                }
                var imageResult = await _cloudinary.AddPhotoAsync(sportEventVM.Image);

                var sportEvent = new SportEvent
                {
                    Id = id,
                    Name = sportEventVM.Name,
                    Description = sportEventVM.Description,
                    Image = imageResult.Url.ToString(),
                    AddressId = sportEventVM.AddressId,
                    AppUserId = sportEventVM.AppUserId,
                    Address = sportEventVM.Address
                };

                _sportEventRepository.Update(sportEvent);
                return RedirectToAction("Index");
            }
            else
            {
                return View(sportEventVM);
            }



        }
    }
}
