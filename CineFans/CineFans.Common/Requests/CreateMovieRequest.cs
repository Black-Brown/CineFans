using Microsoft.AspNetCore.Http;

namespace CineFans.Common.Requests
{
    public class CreateMovieRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Director { get; set; } = string.Empty;
        public int GenreId { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
