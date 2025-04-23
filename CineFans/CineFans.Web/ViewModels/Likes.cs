using Microsoft.EntityFrameworkCore;

namespace CineFans.Web.ViewModels
{
    [PrimaryKey(nameof(PostId), nameof(UserId))]
    public class Likes
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}
