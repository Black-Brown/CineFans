using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineFansApp.Domain.Entities
{
    internal class Movie
    {
        public int PeliculaId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Anio { get; set; }
        public string Director { get; set; }
        public int GeneroId { get; set; }
        public string ImagenUrl { get; set; }

        // Navigation properties
        public Genre Genre { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
