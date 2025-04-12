using System.ComponentModel.DataAnnotations;

namespace CinesFansApp.API.Models
{
    public class PostRequest
    {
        [Required]
        public string? Texto { get; set; }

        [Required]
        public int PeliculaId { get; set; }
    }
}
