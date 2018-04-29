using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TravelWebApp.ViewModels;

namespace TravelWebApp.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManagerService;

        public AccountController(UserManager<IdentityUser> userManagerService)
        {
            _userManagerService = userManagerService;
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
                    // redirect to Homepage.
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
    }
}