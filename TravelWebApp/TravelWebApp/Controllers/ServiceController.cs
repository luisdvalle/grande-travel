using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelWebApp.Models;
using TravelWebApp.Services;

namespace TravelWebApp.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/travelpackages/")]
    public class ServiceController : Controller
    {
        private IDataService<TravelPackage> _travelPackageDataService;

        public ServiceController(IDataService<TravelPackage> travelPackageDataService)
        {
            _travelPackageDataService = travelPackageDataService;
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<TravelPackageResult> GetAllTravelPackages()
        {
            return _travelPackageDataService.GetAll()
                .Select
                (
                    tp => new TravelPackageResult
                    {
                        Description = tp.Description,
                        Location = tp.Location,
                        Name = tp.Name,
                        Price = tp.Price
                    }
                );
        }

        [HttpGet]
        [Route("all/filter")]
        public IEnumerable<TravelPackageResult> GetTravelPackage([FromQuery] string location)
        {
            return _travelPackageDataService.GetAll()
                .Where(tp => tp.Location.ToLower() == location.ToLower())
                .Select
                (
                    tp => new TravelPackageResult
                    {
                        Description = tp.Description,
                        Location = tp.Location,
                        Name = tp.Name,
                        Price = tp.Price
                    }
                );
        }
    }
}