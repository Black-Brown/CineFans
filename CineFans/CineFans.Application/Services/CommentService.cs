using CineFans.Application.Contracts;
using CineFans.Common.Dtos;
using CineFans.Domain.Entities;
using CineFans.Infrastructure.Interfaces;
using AutoMapper;

namespace CineFans.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<CommentDto> CreateCommentAsync(CommentDto commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            comment.CreatedAt = DateTime.UtcNow;

            await _commentRepository.AddAsync(comment);
            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<List<CommentDto>> GetCommentsByMovieIdAsync(int movieId)
        {
            var comments = await _commentRepository.GetByMovieIdAsync(movieId);
            return _mapper.Map<List<CommentDto>>(comments);
        }

        public async Task<List<CommentDto>> GetCommentsByUserIdAsync(int userId)
        {
            var comments = await _commentRepository.GetByUserIdAsync(userId);
            return _mapper.Map<List<CommentDto>>(comments);
        }
    }
}

