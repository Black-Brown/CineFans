using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CineFans.Web.ViewModels
{
    public class CommentsViewModel
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "El comentario no puede superar los 1000 caracteres.")]
        public string Text { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string? UserName { get; set; }
        public string? MovieTitle { get; set; }

        // Para cargar los Select
        public List<SelectListItem>? Users { get; set; }
        public List<SelectListItem>? Movies { get; set; }
    }
}
