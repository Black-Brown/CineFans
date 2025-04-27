namespace CineFans.Web.ViewModels
{
    public class MovieWithCommentsViewModel
    {
        public int MovieId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Year { get; set; }
        public string? Director { get; set; }
        public string? GenreName { get; set; }
        public string? ImageUrl { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }

    public class CommentViewModel
    {
        public int UserId { get; set; }
        public string? Text { get; set; }
        public DateTime Date { get; set; }
    }
}
