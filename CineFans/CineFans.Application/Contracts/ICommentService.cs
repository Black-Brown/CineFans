using CineFans.Common.Dtos;

namespace CineFans.Application.Contracts
{
    public interface ICommentService
    {
        Task<CommentDto> CreateCommentAsync(CommentDto commentDto);
        Task<CommentDto> GetCommentByIdAsync(int commentId);
        Task<List<CommentDto>> GetAllCommentsAsync();
        Task<List<CommentDto>> GetCommentsByMovieIdAsync(int movieId);
        Task<List<CommentDto>> GetCommentsByUserIdAsync(int UserId);
        Task<List<MovieDto>> GetMoviesWithCommentsAsync();

    }
}
