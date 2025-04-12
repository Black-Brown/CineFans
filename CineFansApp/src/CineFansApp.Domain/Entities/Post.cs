using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineFansApp.Domain.Entities
{
    public class Post
    {
<<<<<<< Updated upstream
=======
        public Post()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
            Texto = string.Empty;
            User = new User();
            Movie = new Movie();
        }

>>>>>>> Stashed changes
        public int PublicacionId { get; set; }
        public int UsuarioId { get; set; }
        public int PeliculaId { get; set; }
        public string? Texto { get; set; }
        public DateTime Fecha { get; set; }

        // Navigation properties (nullable, porque EF las rellena luego)
        public virtual User? User { get; set; }
        public virtual Movie? Movie { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Like>? Likes { get; set; } = new List<Like>();
    }
}
