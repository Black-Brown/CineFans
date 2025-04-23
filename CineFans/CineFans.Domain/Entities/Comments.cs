using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineFans.Domain.Entities
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        // Propiedades de navegación
        public Post Post { get; set; } = new Post();
        public User User { get; set; } = new User();
    }
}