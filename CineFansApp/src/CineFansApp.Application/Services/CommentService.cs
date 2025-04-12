using CineFansApp.Application.Interfaces;
using CineFansApp.Domain.DTOs;
using CineFansApp.Domain.Entities;
using CineFansApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineFansApp.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommentDto?> GetCommentByIdAsync(int commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
                return null;

            return MapToDto(comment);
        }

        public async Task<List<CommentDto>> GetCommentsByPostIdAsync(int postId)
        {
            var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);
            return comments.Select(MapToDto).ToList();
        }

        public async Task<CommentDto?> CreateCommentAsync(CommentDto commentDto)
        {
            var comment = new Comment
            {
                PublicacionId = commentDto.PublicacionId,
                UsuarioId = commentDto.UsuarioId,
                Texto = commentDto.Texto ?? string.Empty,
                Fecha = DateTime.Now
            };

            _commentRepository.Add(comment);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(comment);
        }

        public async Task<CommentDto?> UpdateCommentAsync(CommentDto commentDto)
        {
            var comment = await _commentRepository.GetByIdAsync(commentDto.ComentarioId);
            if (comment == null)
                throw new KeyNotFoundException($"Comentario con ID {commentDto.ComentarioId} no encontrado");

            comment.Texto = commentDto.Texto ?? string.Empty;

            await _unitOfWork.SaveChangesAsync();
            return MapToDto(comment);
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
                throw new KeyNotFoundException($"Comentario con ID {commentId} no encontrado");

            _commentRepository.Remove(comment);
            await _unitOfWork.SaveChangesAsync();
        }

        private CommentDto MapToDto(Comment comment)
        {
            return new CommentDto
            {
                ComentarioId = comment.ComentarioId,
                PublicacionId = comment.PublicacionId,
                UsuarioId = comment.UsuarioId,
                NombreUsuario = comment.User?.Nombre,
                FotoPerfilUsuario = comment.User?.FotoPerfil,
                Texto = comment.Texto,
                Fecha = comment.Fecha
            };
        }
    }
}
