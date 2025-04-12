using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CineFansApp.Frontend.ViewModels
{
    public class PostCreateVM
    {
        [Required(ErrorMessage = "El texto de la publicación es requerido")]
        public required string Texto { get; set; }

        [Required(ErrorMessage = "Debes seleccionar una película")]
        public int PeliculaId { get; set; }

        public required List<SelectListItem> Peliculas { get; set; }
    }
}
