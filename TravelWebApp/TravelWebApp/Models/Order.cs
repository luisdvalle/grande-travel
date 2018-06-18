using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public int ProfileId { get; set; }
        public string TravelPackageId { get; set; }
        //public ICollection<TravelPackageOrder> TravelPackageOrders { get; set; }
    }
}
