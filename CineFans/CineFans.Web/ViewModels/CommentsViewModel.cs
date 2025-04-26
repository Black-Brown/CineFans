using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CineFans.Web.ViewModels
{
    public class CommentsViewModel
    {
        [Key]
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string? UserName { get; set; }
        public string? MovieTitle { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public int MovieId { get; set; }

        public List<SelectListItem>? Users { get; set; }
        public List<SelectListItem>? Movies { get; set; }
    }
}
