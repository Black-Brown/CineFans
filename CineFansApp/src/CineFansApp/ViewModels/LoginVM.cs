using System.ComponentModel.DataAnnotations;

namespace CineFansApp.Frontend.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RecordarMe { get; set; }
    }
}
