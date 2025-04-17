using System;
using System.Collections.Generic;

namespace CineFansApp.Domain.Entities
{
    public class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
            Texto = string.Empty;
            User = new User();
            Movie = new Movie();
        }

        public int PublicacionId { get; set; }
        public int UsuarioId { get; set; }
        public int PeliculaId { get; set; }
        public string? Texto { get; set; }
        public DateTime Fecha { get; set; }

        // Propiedades de navegación
        public virtual User? User { get; set; }
        public virtual Movie? Movie { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<Like>? Likes { get; set; }
    }
}
