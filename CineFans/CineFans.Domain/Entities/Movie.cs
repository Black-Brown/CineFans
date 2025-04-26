using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineFans.Domain.Entities
{
    [Table("Movies")]
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Range(1800, 2100)]
        public int Year { get; set; }

        [Required]
        [StringLength(100)]
        public string Director { get; set; } = string.Empty;

        [Required]
        public string GenreName { get; set; } = string.Empty;

        [StringLength(255)]
        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        // Navigation properties
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>(); // Agregado
    }
}
