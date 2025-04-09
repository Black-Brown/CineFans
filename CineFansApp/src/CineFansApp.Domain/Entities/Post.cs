using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineFansApp.Domain.Entities
{
    internal class Post
    {
        public int PublicacionId { get; set; }
        public int UsuarioId { get; set; }
        public int PeliculaId { get; set; }
        public string Texto { get; set; }
        public DateTime Fecha { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Movie Movie { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
