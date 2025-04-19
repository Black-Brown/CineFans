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
    }
}