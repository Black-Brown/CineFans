namespace CineFans.Domain.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public Movie Movie { get; set; } = new Movie();
        public User User { get; set; } = new User();
    }
}
