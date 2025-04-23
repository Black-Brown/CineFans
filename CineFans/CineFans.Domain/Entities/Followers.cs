using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineFans.Domain.Entities
{
    [Table("Followers")]
    [PrimaryKey(nameof(FollowerId), nameof(FollowedId))]
    public class Follower
    {
        public int FollowerId { get; set; }
        public int FollowedId { get; set; }

        // Propiedades de navegación
        public User FollowerUser { get; set; } = new User();
        public User FollowedUser { get; set; } = new User();
    }
}
