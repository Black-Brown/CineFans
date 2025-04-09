using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineFansApp.Domain.Entities
{
    internal class Genre
    {
        public int GeneroId { get; set; }
        public string Nombre { get; set; }

        // Navigation property
        public ICollection<Movie> Movies { get; set; }
    }
}
