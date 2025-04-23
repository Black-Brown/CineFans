using CineFans.Application.Contracts;
using CineFans.Common.Dtos;
using CineFans.Common.Requests;
using CineFans.Common.Responses;
using CineFans.Domain.Entities;
using CineFans.Infrastructure.Interface;

namespace CineFans.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IGenericRepository<Comment> _commentRepository;

        public CommentService(IGenericRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<List<CommentDto>> GetAllAsync()
        {
            var comments = await _commentRepository.GetAllAsync();

            return comments.Select(c => new CommentDto
            {
                CommentId = c.CommentId,
                PostId = c.PostId,
                UserId = c.UserId,
                Text = c.Text,
                Date = c.Date
            }).ToList();
        }

        public async Task<CommentDto?> GetByIdAsync(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment == null)
                return null;

            return new CommentDto
            {
                CommentId = comment.CommentId,
                PostId = comment.PostId,
                UserId = comment.UserId,
                Text = comment.Text,
                Date = comment.Date
            };
        }

        public async Task<BaseResponse<string>> CreateAsync(CreateCommentRequest request)
        {
            var comment = new Comment
            {
                PostId = request.PostId,
                UserId = request.UserId,
                Text = request.Text,
                Date = DateTime.UtcNow
            };

            var result = await _commentRepository.CreateAsync(comment);

            if (!result)
                return new BaseResponse<string> { Success = false, Message = "No se pudo crear el comentario." };

            return new BaseResponse<string> { Success = true, Message = "Comentario creado exitosamente." };
        }

        public async Task<BaseResponse<string>> UpdateAsync(UpdateCommentRequest request)
        {
            var comment = await _commentRepository.GetByIdAsync(request.CommentId);

            if (comment == null)
                return new BaseResponse<string> { Success = false, Message = "Comentario no encontrado." };

            comment.Text = request.Text;
            comment.Date = DateTime.UtcNow;

            var result = await _commentRepository.UpdateAsync(comment);

            if (!result)
                return new BaseResponse<string> { Success = false, Message = "No se pudo actualizar el comentario." };

            return new BaseResponse<string> { Success = true, Message = "Comentario actualizado exitosamente." };
        }

        public async Task<BaseResponse<string>> DeleteAsync(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment == null)
                return new BaseResponse<string> { Success = false, Message = "Comentario no encontrado." };

            var result = await _commentRepository.DeleteAsync(comment);

            if (!result)
                return new BaseResponse<string> { Success = false, Message = "No se pudo eliminar el comentario." };

            return new BaseResponse<string> { Success = true, Message = "Comentario eliminado exitosamente." };
        }
    }
}
