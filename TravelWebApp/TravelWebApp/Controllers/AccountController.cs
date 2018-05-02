using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelWebApp.Models;
using TravelWebApp.Services;
using TravelWebApp.ViewModels;

namespace TravelWebApp.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManagerService;
        private SignInManager<IdentityUser> _signInManagerService;
        private IDataService<Profile> _profileDataService;

        public AccountController(
            UserManager<IdentityUser> userManagerService, 
            SignInManager<IdentityUser> signInManagerService,
            IDataService<Profile> profileDataService
            )
        {
            _userManagerService = userManagerService;
            _signInManagerService = signInManagerService;
            _profileDataService = profileDataService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // adding new user to DB.
                IdentityUser user = new IdentityUser(vm.Username);
                user.Email = vm.Email;
                IdentityResult result = await _userManagerService.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    // Add default Profile.
                    Profile profile = new Profile
                    {
                        FirstName = "Enter your first name",
                        LastName = "Enter your last name",
                        Email = vm.Email,
                        PhoneNumber = "Enter your phone number",
                        UserId = user.Id
                    };
                    _profileDataService.Create(profile);

                    // Redirect to Homepage.
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // show errors.
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            AccountLoginViewModel vm = new AccountLoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var results = await _signInManagerService.PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, false);

                if (results.Succeeded)
                {
                    if (!string.IsNullOrEmpty(vm.ReturnUrl))
                    {
                        return RedirectToAction(vm.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Username or Password incorrect");
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManagerService.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult UpdateProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(AccountUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                
            }

            return View(vm);
        }
    }
}