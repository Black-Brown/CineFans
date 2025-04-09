using CineFansApp.Application.Interfaces;
using CineFansApp.Domain.DTOs;
using CineFansApp.Frontend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace CineSocial.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMovieService _movieService;
        private readonly ICommentService _commentService;

        public PostController(
            IPostService postService,
            IMovieService movieService,
            ICommentService commentService)
        {
            _postService = postService;
            _movieService = movieService;
            _commentService = commentService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            var viewModel = new PostCreateVM
            {
                Peliculas = movies.Select(m => new SelectListItem
                {
                    Value = m.PeliculaId.ToString(),
                    Text = m.Titulo
                }).ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(PostCreateVM viewModel)
        {
            if (ModelState.IsValid)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                
                var postDto = new PostDto
                {
                    UsuarioId = userId,
                    PeliculaId = viewModel.PeliculaId,
                    Texto = viewModel.Texto,
                    Fecha = DateTime.Now
                };
                
                await _postService.CreatePostAsync(postDto);
                return RedirectToAction("Index", "Home");
            }
            
            // Si hay errores, volver a cargar la lista de películas
            var movies = await _movieService.GetAllMoviesAsync();
            viewModel.Peliculas = movies.Select(m => new SelectListItem
            {
                Value = m.PeliculaId.ToString(),
                Text = m.Titulo
            }).ToList();
            
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(int postId, string commentText)
        {
            if (string.IsNullOrWhiteSpace(commentText))
            {
                return RedirectToAction(nameof(Details), new { id = postId });
            }
            
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            var commentDto = new CommentDto
            {
                PublicacionId = postId,
                UsuarioId = userId,
                Texto = commentText,
                Fecha = DateTime.Now
            };
            
            await _commentService.CreateCommentAsync(commentDto);
            return RedirectToAction(nameof(Details), new { id = postId });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Like(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _postService.LikePostAsync(id, userId);
            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Unlike(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _postService.UnlikePostAsync(id, userId);
            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var post = await _postService.GetPostByIdAsync(id);
            
            if (post == null || post.UsuarioId != userId)
            {
                return Forbid();
            }
            
            await _postService.DeletePostAsync(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
