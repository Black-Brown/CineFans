using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineFans.Domain.Entities
{
    [Table("Movies")]
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Director { get; set; } = string.Empty;
        public int GenreId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;


        [NotMapped] // No mapear a la base de datos
        public IFormFile? ImageFile { get; set; } // Nueva propiedad para el archivo

        // Propiedades de navegación
        public Genre Genre { get; set; } = new Genre();
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}