using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineFans.Application.Dtos
{
    internal class UsersRegisterDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string FotoPerfil { get; set; } = string.Empty; // puede ser una URL base64 o nombre de archivo
    }
}
