namespace CineFans.Domain.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public string? Text { get; set; }
        public DateTime Date { get; set; }

        public Movie? Movie { get; set; }
        public User? User { get; set; }

    }
}
