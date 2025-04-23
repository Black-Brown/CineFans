using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore; // Asegúrate de tener esta referencia

namespace CineFans.Web.ViewModels
{
    [PrimaryKey(nameof(FollowerId), nameof(FollowedId))]
    public class Followers
    {
        public int FollowerId { get; set; }
        public int FollowedId { get; set; }
    }
}