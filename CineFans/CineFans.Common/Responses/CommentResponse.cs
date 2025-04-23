namespace CineFans.Common.Responses
{
    public class CommentResponse
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
