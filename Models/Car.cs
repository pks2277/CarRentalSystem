using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class Car
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public decimal PricePerDay { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
    }
}
