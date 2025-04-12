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
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentService _commentService;
        private readonly IUnitOfWork _unitOfWork;

        public PostService(
            IPostRepository postRepository,
            ICommentService commentService,
            IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _commentService = commentService;
            _unitOfWork = unitOfWork;
        }

        public async Task<PostDto?> GetPostByIdAsync(int postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null)
                return null;

            var postDto = MapToDto(post);
            
            // Cargar comentarios
            postDto.Comentarios = await _commentService.GetCommentsByPostIdAsync(postId);
            
            return postDto;
        }

        public async Task<List<PostDto>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            return posts.Select(MapToDto).ToList();
        }

        public async Task<List<PostDto>> GetRecentPostsAsync(int count)
        {
            var posts = await _postRepository.GetRecentPostsAsync(count);
            return posts.Select(MapToDto).ToList();
        }

        public async Task<List<PostDto>> GetFeedPostsAsync(int userId)
        {
            var posts = await _postRepository.GetFeedPostsAsync(userId);
            var dtos = posts.Select(p => MapToDto(p)).ToList();
            
            // Marcar los posts que el usuario ha dado like
            foreach (var post in dtos)
            {
                post.UserLiked = await _postRepository.HasUserLikedPostAsync(post.PublicacionId, userId);
            }
            
            return dtos;
        }

        public async Task<List<PostDto>> GetPostsByUserIdAsync(int userId)
        {
            var posts = await _postRepository.GetPostsByUserIdAsync(userId);
            return posts.Select(MapToDto).ToList();
        }

        public async Task<List<PostDto>> GetPostsByMovieIdAsync(int movieId)
        {
            var posts = await _postRepository.GetPostsByMovieIdAsync(movieId);
            return posts.Select(MapToDto).ToList();
        }

        public async Task<PostDto?> CreatePostAsync(PostDto postDto)
        {
            var post = new Post
            {
                UsuarioId = postDto.UsuarioId,
                PeliculaId = postDto.PeliculaId,
                Texto = postDto.Texto ?? string.Empty,
                Fecha = DateTime.Now
            };

            _postRepository.Add(post);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(post);
        }

        public async Task<PostDto?> UpdatePostAsync(PostDto postDto)
        {
            var post = await _postRepository.GetByIdAsync(postDto.PublicacionId);
            if (post == null)
                throw new KeyNotFoundException($"Publicación con ID {postDto.PublicacionId} no encontrada");

            post.Texto = postDto.Texto ?? string.Empty;

            await _unitOfWork.SaveChangesAsync();
            return MapToDto(post);
        }

        public async Task DeletePostAsync(int postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null)
                throw new KeyNotFoundException($"Publicación con ID {postId} no encontrada");

            _postRepository.Remove(post);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task LikePostAsync(int postId, int userId)
        {
            if (await _postRepository.HasUserLikedPostAsync(postId, userId))
                return;

            await _postRepository.AddLikeAsync(postId, userId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UnlikePostAsync(int postId, int userId)
        {
            if (!await _postRepository.HasUserLikedPostAsync(postId, userId))
                return;

            await _postRepository.RemoveLikeAsync(postId, userId);
            await _unitOfWork.SaveChangesAsync();
        }

        private PostDto MapToDto(Post post)
        {
            return new PostDto
            {
                PublicacionId = post.PublicacionId,
                UsuarioId = post.UsuarioId,
<<<<<<< Updated upstream
<<<<<<< Updated upstream
                NombreUsuario = post.User?.Nombre,
                FotoPerfilUsuario = post.User?.FotoPerfil,
                TituloPelicula = post.Movie?.Titulo,
                ImagenPelicula = post.Movie?.ImagenUrl,
=======
=======
>>>>>>> Stashed changes
                NombreUsuario = post.User?.Nombre ?? string.Empty,
                FotoPerfilUsuario = post.User?.FotoPerfil ?? string.Empty,
                PeliculaId = post.PeliculaId,
                TituloPelicula = post.Movie?.Titulo ?? string.Empty,
                ImagenPelicula = post.Movie?.ImagenUrl ?? string.Empty,
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
                Texto = post.Texto,
                Fecha = post.Fecha,
                LikesCount = post.Likes?.Count ?? 0,
                ComentariosCount = post.Comments?.Count ?? 0,
                UserLiked = false // Se establece en GetFeedPostsAsync
            };
        }
    }
}
