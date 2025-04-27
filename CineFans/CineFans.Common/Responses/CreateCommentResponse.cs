using CineFans.Common.Dtos;

namespace CineFans.Common.Responses
{
    public class CreateCommentResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public CommentDto? Comment { get; set; }
    }
}
