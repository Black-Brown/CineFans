using CineFans.Common.Dtos;
using CineFans.Common.Requests;
using CineFans.Common.Responses;

namespace CineFans.Application.Contracts
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetAllAsync();
        Task<CommentDto?> GetByIdAsync(int id);
        Task<BaseResponse<string>> CreateAsync(CreateCommentRequest request);
        Task<BaseResponse<string>> UpdateAsync(UpdateCommentRequest request);
        Task<BaseResponse<string>> DeleteAsync(int id);
    }
}
