namespace CineFans.Common.Requests
{
    public class CreateCommentRequest
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
