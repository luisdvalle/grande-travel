using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelWebApp.Models
{
    public class TravelPackage
    {
        public string TravelPackageId { get; set; }
        [Required]
        public int ProfileId { get; set; }
        //public Profile Profile { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Activated { get; set; }
        public ICollection<TravelPackageOrder> TravelPackageOrders { get; set; }
    }
}
