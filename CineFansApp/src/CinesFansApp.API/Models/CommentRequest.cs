using System.ComponentModel.DataAnnotations;

namespace CinesFansApp.API.Models
{
    public class CommentRequest
    {
        [Required]
        public int PublicacionId { get; set; }

        [Required]
        public string Texto { get; set; }
    }
}
