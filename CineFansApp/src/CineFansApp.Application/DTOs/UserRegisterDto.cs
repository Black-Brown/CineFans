namespace CineFansApp.Application.DTOs
{
    public class UserRegisterDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string FotoPerfil { get; set; } = string.Empty; // puede ser una URL base64 o nombre de archivo
    }
}
