using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }  // either Admin and User
    }
}
