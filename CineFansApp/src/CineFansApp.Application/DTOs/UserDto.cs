namespace CineFansApp.Application.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FotoPerfil { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }
}
