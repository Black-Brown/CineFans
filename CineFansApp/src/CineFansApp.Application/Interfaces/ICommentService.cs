using CineFansApp.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineFansApp.Application.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDto> GetCommentByIdAsync(int commentId);
        Task<List<CommentDto>> GetCommentsByPostIdAsync(int postId);
        Task<CommentDto> CreateCommentAsync(CommentDto commentDto);
        Task<CommentDto> UpdateCommentAsync(CommentDto commentDto);
        Task DeleteCommentAsync(int commentId);
    }
}
