using CineFans.Common.Dtos;

namespace CineFans.Application.Contracts
{
    public interface ICommentService
    {
        Task<CommentDto> CreateCommentAsync(CommentDto commentDto);
        Task<List<CommentDto>> GetCommentsByMovieIdAsync(int movieId);
        Task<List<CommentDto>> GetCommentsByUserIdAsync(int userId);
    }
}
