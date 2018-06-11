using System.ComponentModel.DataAnnotations;

namespace TravelWebApp.ViewModels
{
    public class TravelPackageUpdateViewModel
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
        public string TravelPackageId { get; set; }
    }
}
