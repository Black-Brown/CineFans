using CineFans.Application.Contracts;
using CineFans.Common.Dtos;
using CineFans.Common.Requests;
using CineFans.Domain.Entities;
using CineFans.Infrastructure.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineFans.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<List<CommentDto>> GetAllAsync()
        {
            var comments = await _commentRepository.GetAllAsync();
            return comments.Select(c => new CommentDto
            {
                CommentId = c.CommentId,
                UserId = c.UserId,
                MovieId = c.MovieId,
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
                UserId = comment.UserId,
                MovieId = comment.MovieId,
                Text = comment.Text,
                Date = comment.Date
            };
        }

        public async Task<bool> CreateAsync(CreateCommentRequest request)
        {
            var comment = new Comment
            {
                UserId = request.UserId,
                MovieId = request.MovieId,
                Text = request.Text,
                Date = DateTime.UtcNow
            };

            return await _commentRepository.CreateAsync(comment);
        }

        public async Task<bool> UpdateAsync(UpdateCommentRequest request)
        {
            var existing = await _commentRepository.GetByIdAsync(request.CommentId);
            if (existing == null)
                return false;

            existing.Text = request.Text;
            return await _commentRepository.UpdateAsync(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
                return false;

            return await _commentRepository.DeleteAsync(comment);
        }

    }
}
