using System;

namespace CineFans.Domain.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public int PostId { get; set; }  // Agregado

        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Relaciones de navegación
        public User User { get; set; } = null!;
        public Movie Movie { get; set; } = null!;
        public Post Post { get; set; } = null!; // Agregado
    }

}
