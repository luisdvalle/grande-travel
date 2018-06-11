using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelWebApp.Models;
using TravelWebApp.Services;
using TravelWebApp.ViewModels;

namespace TravelWebApp.Controllers
{
    
    public class TravelPackageController : Controller
    {
        private IDataService<TravelPackage> _travelPackageDataService;
        private IDataService<Profile> _profileDataService;
        private UserManager<IdentityUser> _userDataService;

        public TravelPackageController(
            IDataService<TravelPackage> travelPackageDataService,
            IDataService<Profile> profileDataService,
            UserManager<IdentityUser> userDataService)
        {
            _travelPackageDataService = travelPackageDataService;
            _profileDataService = profileDataService;
            _userDataService = userDataService;
        }

        [HttpGet]
        public IActionResult Search()
        {
            List<TravelPackage> travelPackages = _travelPackageDataService
                .GetAll().ToList();

            travelPackages = travelPackages.Where(tp => tp.Activated == true).ToList();

            TravelPackageSearchViewModel vm = new TravelPackageSearchViewModel
            {
                Location = null,
                MaxPrice = null,
                MinPrice = null,
                NumTravelPackages = travelPackages.Count,
                Ordered = false,
                TravelPackages = travelPackages
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Search(TravelPackageSearchViewModel vm)
        {
            if (ModelState.IsValid)
            {
                List<TravelPackage> travelPackages = _travelPackageDataService
                .GetAll().ToList();

                travelPackages = travelPackages.Where(tp => tp.Activated == true).ToList();

                if (!string.IsNullOrEmpty(vm.Location))
                {
                    travelPackages = travelPackages.Where(tp => tp.Location.ToLower() == vm.Location.ToLower()).ToList();
                }

                if (vm.Ordered)
                {
                    travelPackages = travelPackages.OrderBy(tp => tp.Price).ToList();
                }

                if (vm.MinPrice != null && vm.MaxPrice != null)
                {
                    travelPackages = travelPackages
                        .Where(tp => tp.Price >= vm.MinPrice && tp.Price <= vm.MaxPrice)
                        .ToList();
                }
                else if (vm.MinPrice != null && vm.MaxPrice == null)
                {
                    travelPackages = travelPackages
                        .Where(tp => tp.Price >= vm.MinPrice)
                        .ToList();
                }
                else if (vm.MinPrice == null && vm.MaxPrice != null)
                {
                    travelPackages = travelPackages
                        .Where(tp => tp.Price <= vm.MaxPrice)
                        .ToList();
                }

                vm.NumTravelPackages = travelPackages.Count;
                vm.TravelPackages = travelPackages;

                return View(vm);
            }
            
            return View(vm);
        }

        [Authorize(Roles = "Provider")]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var user = await _userDataService.GetUserAsync(User);

                var profile = _profileDataService.GetSingle(p => p.UserId == user.Id);

                // Get all TravelPackages belonging to Profile
                List<TravelPackage> travelPackages = _travelPackageDataService
                    .Query(tp => tp.ProfileId == profile.ProfileId).ToList();

                TravelPackageAllViewModel vm = new TravelPackageAllViewModel
                {
                    NumberTravelPackages = travelPackages.Count,
                    TravelPackages = travelPackages
                };

                return View(vm);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                ModelState.AddModelError("", "Error while processing request: " + errorMessage);

                return View();
            }
        }

        [Authorize(Roles = "Provider")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Provider")]
        [HttpPost]
        public async Task<IActionResult> Add(TravelPackageAddViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userDataService.GetUserAsync(HttpContext.User);

                    var profile = _profileDataService.GetSingle(p => p.UserId == user.Id);

                    TravelPackage travelPackage = new TravelPackage
                    {
                        Activated = vm.Activated,
                        Description = vm.Description,
                        Location = vm.Location,
                        Name = vm.Name,
                        Price = vm.Price,
                        ProfileId = profile.ProfileId,
                    };

                    _travelPackageDataService.Create(travelPackage);

                    // TODO: redirect to view displaying all travel packages belonging to this user
                    return RedirectToAction("All", "TravelPackage");

                }
                catch (Exception ex)
                {
                    var errorMessage = ex.Message;
                    ModelState.AddModelError("", "Error while processing request: " + errorMessage);

                    return View(vm);
                }
            }

            return View(vm);
        }

        [Authorize(Roles = "Provider")]
        [HttpGet]
        public IActionResult Update(string id)
        {
            var travelPackage = _travelPackageDataService.GetSingle(tp => tp.TravelPackageId == id);

            var updateViewModel = new TravelPackageUpdateViewModel
            {
                Activated = travelPackage.Activated,
                Description = travelPackage.Description,
                Location = travelPackage.Location,
                Name = travelPackage.Name,
                Price = travelPackage.Price,
                TravelPackageId = travelPackage.TravelPackageId
            };

            return View(updateViewModel);
        }

        [Authorize(Roles = "Provider")]
        [HttpPost]
        public IActionResult Update(TravelPackageUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var travelPackage = _travelPackageDataService
                    .GetSingle(tp => tp.TravelPackageId == vm.TravelPackageId);

                if (travelPackage != null)
                {
                    travelPackage.Activated = vm.Activated;
                    travelPackage.Description = vm.Description;
                    travelPackage.Location = vm.Location;
                    travelPackage.Name = vm.Name;
                    travelPackage.Price = vm.Price;
                    
                    _travelPackageDataService.Update(travelPackage);
                }

                return RedirectToAction("All", "TravelPackage");
            }

            return View(vm);
        }
    }
}