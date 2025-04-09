using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineFansApp.Domain.Entities
{
    public class Genre
    {
        public Genre()
        {
            Movies = new HashSet<Movie>();
        }

        public int GeneroId { get; set; }
        public string Nombre { get; set; }

        // Navigation property
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
