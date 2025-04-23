using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CineFans.Web.ViewModels
{
    public class MovieViewModel
    {
        public int MovieId { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "El año es obligatorio")]
        [Range(1900, 2100)]
        public int Year { get; set; }

        [Required(ErrorMessage = "El director es obligatorio")]
        public string Director { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un género")]
        [Display(Name = "Género")]
        public int GenreId { get; set; }

        public GenreViewModel Genre { get; set; } = new GenreViewModel();

        [Required(ErrorMessage = "La imagen es obligatoria")]
        [DataType(DataType.Upload)]
        public IFormFile? ImageFile { get; set; }

        public string ImageUrl { get; set; } = string.Empty;
    }
}