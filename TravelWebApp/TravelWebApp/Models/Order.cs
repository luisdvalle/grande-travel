using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelWebApp.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public int NumberPersons { get; set; }
        [Required]
        public double ItemPrice { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        [Required]
        public string ProfileId { get; set; }
        public ICollection<TravelPackageOrder> TravelPackageOrders { get; set; }
    }
}
