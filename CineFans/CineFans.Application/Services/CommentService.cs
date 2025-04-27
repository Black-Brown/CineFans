using CineFans.Application.Contracts;
using CineFans.Common.Dtos;
using CineFans.Domain.Entities;
using AutoMapper;
using CineFans.Infrastructure.Interface;

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
            var comment = new Comment
            {
                Text = commentDto.Text,
                MovieId = commentDto.MovieId,
                UserId = commentDto.UserId,
                Date = DateTime.Now
            };

            await _commentRepository.AddAsync(comment);

            // Opcional: Mapear de vuelta
            commentDto.CommentId = comment.CommentId;
            commentDto.Date = comment.Date;

            return commentDto;
        }

        public async Task<List<CommentDto>> GetCommentsByMovieIdAsync(int movieId)
        {
            var comments = await _commentRepository.GetCommentsByMovieIdAsync(movieId);
            return _mapper.Map<List<CommentDto>>(comments);
        }

        public async Task<List<CommentDto>> GetCommentsByUserIdAsync(int UserId)
        {
            var comments = await _commentRepository.GetCommentsByUserIdAsync(UserId);
            return _mapper.Map<List<CommentDto>>(comments);
        }

        public async Task<CommentDto> GetCommentByIdAsync(int commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
            {
                throw new KeyNotFoundException($"Comment with ID {commentId} not found.");
            }

            return new CommentDto
            {
                CommentId = comment.CommentId,
                MovieId = comment.MovieId,
                UserId = comment.UserId,
                Text = comment.Text,
                Date = comment.Date
            };
        }

        public async Task<List<CommentDto>> GetAllCommentsAsync()
        {
            var comments = await _commentRepository.GetAllAsync();
            return _mapper.Map<List<CommentDto>>(comments);

        }

        public Task<List<MovieDto>> GetMoviesWithCommentsAsync()
        {
            throw new NotImplementedException();
        }
    }
}

