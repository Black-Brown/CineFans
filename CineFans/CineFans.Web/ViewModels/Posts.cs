using System.ComponentModel.DataAnnotations;

namespace CineFans.Web.ViewModels
{
    public class PostsViewModel
    {
        [Key]
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
