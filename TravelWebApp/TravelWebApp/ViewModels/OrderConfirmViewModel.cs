using System.ComponentModel.DataAnnotations;

namespace TravelWebApp.ViewModels
{
    public class OrderConfirmViewModel
    {
        public string TravelPackageId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        [Required]
        public int NumPersons { get; set; }
        public double TotalPrice { get; set; }
    }
}
