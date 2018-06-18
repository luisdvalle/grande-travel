using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelWebApp.ViewModels
{
    public class OrderBuyViewModel
    {
        public string TravelPackageId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        [Required]
        public int NumPersons { get; set; }
    }
}
