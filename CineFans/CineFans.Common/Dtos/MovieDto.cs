namespace CineFans.Common.Dtos
{
    public class MovieDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Director { get; set; } = string.Empty;
        public string GenreName { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();  // Lista de comentarios
    }
}
