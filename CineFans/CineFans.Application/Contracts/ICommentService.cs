using CineFans.Common.Dtos;
using CineFans.Common.Requests;

namespace CineFans.Application.Contracts
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAllAsync();
        Task<CommentDto?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateCommentRequest request);
        Task<bool> UpdateAsync(UpdateCommentRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
