namespace CineFans.Common.Requests
{
    public class CreateCommentRequest
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
