using CineFansApp.Domain.DTOs;

namespace CineFansApp.Frontend.ViewModels
{
    public class MovieDetailsVM
    {
        public MovieDto Movie { get; set; }
        public List<PostDto> Posts { get; set; }
    }
}
