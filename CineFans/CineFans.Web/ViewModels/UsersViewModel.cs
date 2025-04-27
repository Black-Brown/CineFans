using System.ComponentModel.DataAnnotations;

namespace CineFans.Web.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        public string? Password { get; set; }  // Asegúrate de que este campo esté presente
        public DateTime RegistrationDate { get; set; }
    }

}
