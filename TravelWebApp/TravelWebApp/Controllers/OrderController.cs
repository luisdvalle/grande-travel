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
    public class OrderController : Controller
    {
        private IDataService<Order> _orderDataService;
        private IDataService<Profile> _profileDataService;
        private IDataService<TravelPackage> _travelPackageDataService;
        //private IDataService<TravelPackageOrder> _travelPackageOrderDataService;
        private UserManager<IdentityUser> _userDataService;

        public OrderController(
            IDataService<Order> orderDataService,
            IDataService<Profile> profileDataService,
            IDataService<TravelPackage> travelPackageService,
            //IDataService<TravelPackageOrder> travelPackageOrderDataService,
            UserManager<IdentityUser> userDataService
            )
        {
            _orderDataService = orderDataService;
            _profileDataService = profileDataService;
            _travelPackageDataService = travelPackageService;
            //_travelPackageOrderDataService = travelPackageOrderDataService;
            _userDataService = userDataService;
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> All()
        {
            try
            {
                var user = await _userDataService.GetUserAsync(User);

                var profile = _profileDataService.GetSingle(p => p.UserId == user.Id);

                // Get all Orders belonging to Profile
                List<Order> orders = _orderDataService
                    .Query(o => o.ProfileId == profile.ProfileId).ToList();

                OrderAllViewModel vm = new OrderAllViewModel
                {
                    NumOrders = orders.Count,
                    Orders = orders
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

        [Authorize(Roles = "Customer")]
        public IActionResult Buy(string id)
        {
            var travelPackage = _travelPackageDataService.GetSingle(tp => tp.TravelPackageId == id);

            // Sanity check. Check if the Travel Package is active
            if (travelPackage != null && travelPackage.Activated)
            {
                OrderBuyViewModel vm = new OrderBuyViewModel
                {
                    Description = travelPackage.Description,
                    Location = travelPackage.Location,
                    Name = travelPackage.Name,
                    NumPersons = 1,
                    Price = travelPackage.Price,
                    TravelPackageId = travelPackage.TravelPackageId
                };

                return View(vm);
            }

            ModelState.AddModelError("", "Travel Package is not active or does not exist. Unable to proceed with this Request.");

            return View(new TravelPackage { Price = 0 });
        }

        [Authorize(Roles = "Customer")]
        public IActionResult Confirm(OrderConfirmViewModel vm)
        {
            var travelPackage = _travelPackageDataService.GetSingle(tp => tp.TravelPackageId == vm.TravelPackageId);

            // Sanity check. Check if the Travel Package is active
            if (travelPackage != null && travelPackage.Activated)
            {

                OrderConfirmViewModel vmConfirm = new OrderConfirmViewModel
                {
                    Description = travelPackage.Description,
                    Location = travelPackage.Location,
                    Name = travelPackage.Name,
                    NumPersons = vm.NumPersons,
                    Price = travelPackage.Price,
                    TravelPackageId = travelPackage.TravelPackageId,
                    TotalPrice = (vm.NumPersons * travelPackage.Price)
                };

                return View(vmConfirm);
            }

            ModelState.AddModelError("", "Travel Package is not active or does not exist. Unable to proceed with this Request.");

            return View(new OrderConfirmViewModel { Price = 0 });
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Process(OrderConfirmViewModel vm)
        {
            try
            {
                var user = await _userDataService.GetUserAsync(User);
                var profile = _profileDataService.GetSingle(p => p.UserId == user.Id);
                var travelPackage = _travelPackageDataService.GetSingle(tp => tp.TravelPackageId == vm.TravelPackageId);

                Order order = new Order
                {
                    ItemPrice = travelPackage.Price,
                    NumberPersons = vm.NumPersons,
                    OrderDate = DateTime.Now,
                    ProfileId = profile.ProfileId,
                    TotalPrice = vm.TotalPrice,
                    TravelPackageId = travelPackage.TravelPackageId
                };

                _orderDataService.Create(order);

                //TravelPackageOrder travelPackageOrder = new TravelPackageOrder
                //{
                //    OrderId = order.OrderId,
                //    TravelPackageId = travelPackage.TravelPackageId
                //};

                //_travelPackageOrderDataService.Create(travelPackageOrder);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                ModelState.AddModelError("", "Error while processing request: " + errorMessage);

                return View();
            }
        }
    }
}