using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CineFans.Web.ViewModels
{
    public class GenreViewModel
    {
        [JsonPropertyName("id")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "El nombre del género es obligatorio")]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;
    }
}
