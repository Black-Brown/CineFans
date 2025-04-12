using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineFansApp.Domain.Entities
{
    public class Movie
    {
        public Movie()
        {
            Posts = new HashSet<Post>();
            Titulo = string.Empty;
            Descripcion = string.Empty;
            Director = string.Empty;
            ImagenUrl = string.Empty;
            Genre = new Genre();
        }

        public int PeliculaId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Anio { get; set; }
        public string Director { get; set; }
        public int GeneroId { get; set; }
        public string ImagenUrl { get; set; }

        // Navigation properties
        public virtual Genre Genre { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
