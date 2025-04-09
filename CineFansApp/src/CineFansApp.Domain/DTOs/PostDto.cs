using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineFansApp.Domain.DTOs
{
    public class PostDto
    {
        public int PublicacionId { get; set; }
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public string FotoPerfilUsuario { get; set; }
        public int PeliculaId { get; set; }
        public string TituloPelicula { get; set; }
        public string ImagenPelicula { get; set; }
        public string Texto { get; set; }
        public DateTime Fecha { get; set; }
        public int LikesCount { get; set; }
        public int ComentariosCount { get; set; }
        public bool UserLiked { get; set; }
        public List<CommentDto> Comentarios { get; set; }
    }
}
