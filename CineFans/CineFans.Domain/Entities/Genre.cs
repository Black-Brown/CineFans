using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineFans.Domain.Entities
{
    [Table("Genres")]
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}