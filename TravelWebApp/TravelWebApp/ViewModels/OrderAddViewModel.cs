using System;
using System.ComponentModel.DataAnnotations;

namespace TravelWebApp.ViewModels
{
    public class OrderAddViewModel
    {
        public DateTime OrderDate { get; set; }
        [Required]
        public int NumberPersons { get; set; }
        public double ItemPrice { get; set; }
        public double TotalPrice { get; set; }
    }
}
