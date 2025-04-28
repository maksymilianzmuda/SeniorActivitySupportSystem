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

        public SportEventController(ApplicationDbContext context, ISportEventRepository sportEventRepository, ICloudinaryService cloudinary)
        {
            
            _sportEventRepository = sportEventRepository;
            _cloudinary = cloudinary;
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
            return View();
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
    }
}
