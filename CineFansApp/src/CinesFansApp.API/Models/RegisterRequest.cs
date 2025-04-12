using System.ComponentModel.DataAnnotations;

namespace CinesFansApp.API.Models
{
    public class RegisterRequest
    {
        [Required]
        public string? Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MinLength(6)]
        public string? Password { get; set; }
    }
}
