using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelWebApp.Models;

namespace TravelWebApp.ViewModels
{
    public class TravelPackageSearchViewModel
    {
        public int NumTravelPackages { get; set; }
        public string Location { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public bool Ordered { get; set; }
        public IList<TravelPackage> TravelPackages { get; set; }
    }
}
