namespace CineFans.Common.Requests
{
    public class CreateMovieRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Director { get; set; } = string.Empty;
        public string GenreName { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
    }
}
