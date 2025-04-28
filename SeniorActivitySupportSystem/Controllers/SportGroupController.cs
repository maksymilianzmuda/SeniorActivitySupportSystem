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
        public SportGroupController(ISportGroupRepository sportGroupRepository, ICloudinaryService cloudinary)
        {

            _sportGroupRepository = sportGroupRepository;

            _cloudinary = cloudinary;
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
            return View();
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
                    Image = result.SecureUrl.ToString(), // <<<<<< NAJWAŻNIEJSZA ZMIANA
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
    }
}
