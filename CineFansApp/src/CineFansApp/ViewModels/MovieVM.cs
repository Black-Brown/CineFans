using Microsoft.AspNetCore.Mvc.Rendering;
using CineFansApp.Domain.DTOs;

namespace CineFansApp.Frontend.ViewModels
{
    public class MovieVM
    {
        public MovieDto Movie { get; set; }
        public List<SelectListItem> Generos { get; set; }
    }
}
