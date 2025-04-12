using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineFansApp.Domain.DTOs
{
    public class CommentDto
    {
        public int ComentarioId { get; set; }
        public int PublicacionId { get; set; }
        public int UsuarioId { get; set; }
        public string? NombreUsuario { get; set; }
        public string? FotoPerfilUsuario { get; set; }
        public string? Texto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
