using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        private RoleManager<IdentityRole> _roleManagerService;
        private IDataService<Profile> _profileDataService;
        private const string CUSTOMER_ROLE_NAME = "Customer";

        public AccountController(
            UserManager<IdentityUser> userManagerService,
            SignInManager<IdentityUser> signInManagerService,
            RoleManager<IdentityRole> roleManagerService,
            IDataService<Profile> profileDataService
            )
        {
            _userManagerService = userManagerService;
            _signInManagerService = signInManagerService;
            _roleManagerService = roleManagerService;
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
                    // Add default role.
                    IdentityRole checkRole = await _roleManagerService.FindByNameAsync(CUSTOMER_ROLE_NAME);
                    if (checkRole != null)
                    {
                        await _userManagerService.AddToRoleAsync(user, CUSTOMER_ROLE_NAME);
                    }
                    else
                    {
                        await _roleManagerService.CreateAsync(new IdentityRole(CUSTOMER_ROLE_NAME));
                        await _userManagerService.AddToRoleAsync(user, CUSTOMER_ROLE_NAME);
                    }

                    // Add default Profile.
                    Profile profile = new Profile
                    {
                        FirstName = "Enter your first name",
                        LastName = "Enter your last name",
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
        [Authorize]
        public async Task<IActionResult> UpdateProfile()
        {
            // Get current user.
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
            // Get profile for current user.
            var profileDb = _profileDataService.GetSingle(p => p.UserId == user.Id);
            // Mappinng to vm.
            AccountUpdateViewModel vm = new AccountUpdateViewModel
            {
                FirstName = profileDb.FirstName,
                LastName = profileDb.LastName,
                Email = (string.IsNullOrEmpty(user.Email)) ? "Enter your email address" : user.Email,
                PhoneNumber = (string.IsNullOrEmpty(user.PhoneNumber)) ? "Enter your phone number" : user.PhoneNumber,
                ProfileId = profileDb.ProfileId
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(AccountUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // Get current user.
                IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);

                user.Email = vm.Email;
                user.PhoneNumber = vm.PhoneNumber;

                // Update User.
                var result = await _userManagerService.UpdateAsync(user);

                if (result.Succeeded)
                {
                    // Mapping to profile.
                    Profile profile = new Profile
                    {
                        FirstName = vm.FirstName,
                        LastName = vm.LastName,
                        ProfileId = vm.ProfileId,
                        UserId = user.Id
                    };

                    // Update Profile.
                    _profileDataService.Update(profile);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(vm);
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddRole(AccountAddRoleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManagerService.CreateAsync(new IdentityRole(vm.RoleName));

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(vm);
        }
    }
}