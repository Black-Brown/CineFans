using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineFans.Domain.Entities
{
    [Table("Likes")]
    [PrimaryKey(nameof(PostId), nameof(UserId))]
    public class Like
    {
        public int PostId { get; set; }
        public int UserId { get; set; }

        // Propiedades de navegación
        public Post Post { get; set; } = new Post();
        public User User { get; set; } = new User();
    }
}