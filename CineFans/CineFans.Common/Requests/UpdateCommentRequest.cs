namespace CineFans.Common.Requests.Comment
{
    public class UpdateCommentRequest
    {
        public int CommentId { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
