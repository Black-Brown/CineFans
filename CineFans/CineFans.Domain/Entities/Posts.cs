using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineFans.Domain.Entities
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        // Propiedades de navegación
        public User User { get; set; } = new User();
        public Movie Movie { get; set; } = new Movie();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
    }
}
