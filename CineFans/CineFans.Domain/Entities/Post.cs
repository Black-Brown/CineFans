using System;
using System.Collections.Generic;

namespace CineFans.Domain.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }

        public string Content { get; set; } = string.Empty;

        // Relaciones de navegación
        public User User { get; set; } = null!;
        public Movie Movie { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>(); // Agregado
    }

}
