using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelWebApp.ViewModels
{
    public class TravelPackageAddViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required, DataType(DataType.Currency)]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Activated { get; set; }
    }
}
