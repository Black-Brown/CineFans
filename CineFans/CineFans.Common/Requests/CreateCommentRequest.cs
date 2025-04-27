namespace CineFans.Common.Requests
{
    public class CreateCommentRequest
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public string? Text { get; set; }
    }
}
