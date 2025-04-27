namespace CineFans.Common.Dtos
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
