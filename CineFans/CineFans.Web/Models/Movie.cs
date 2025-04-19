namespace CineFans.Web.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Director { get; set; } = string.Empty;
        public int GenreId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
