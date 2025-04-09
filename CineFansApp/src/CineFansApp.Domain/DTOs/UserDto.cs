using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineFansApp.Domain.DTOs
{
    internal class UserDto
    {
        public int UserId { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string FotoPerfil { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int SeguidoresCount { get; set; }
        public int SiguiendoCount { get; set; }
        public int PublicacionesCount { get; set; }

    }
}
