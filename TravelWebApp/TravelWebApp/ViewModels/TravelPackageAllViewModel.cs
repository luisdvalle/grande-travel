using System.Collections.Generic;
using TravelWebApp.Models;

namespace TravelWebApp.ViewModels
{
    public class TravelPackageAllViewModel
    {
        public int NumberTravelPackages { get; set; }
        public IList<TravelPackage> TravelPackages { get; set; }
    }
}
