using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelWebApp.Models
{
    public class TravelPackageOrder
    {
        public string TravelPackageId { get; set; }
        public TravelPackage TravelPackage { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
