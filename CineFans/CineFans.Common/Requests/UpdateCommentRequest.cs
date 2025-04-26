namespace CineFans.Common.Requests
{
    public class UpdateCommentRequest
    {
        public int CommentId { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
