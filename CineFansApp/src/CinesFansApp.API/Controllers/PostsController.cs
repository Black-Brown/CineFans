using CineFansApp.Application.Interfaces;
using CineFansApp.Domain.DTOs;
using CinesFansApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CinesFansApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: api/posts
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<PostDto>>>> GetAllPosts()
        {
            try
            {
                var posts = await _postService.GetAllPostsAsync();
                return Ok(ApiResponse<List<PostDto>>.Ok(posts));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<PostDto>>.Error(ex.Message));
            }
        }

        // POST: api/posts
        [HttpPost]
        public async Task<ActionResult<ApiResponse<PostDto>>> CreatePost([FromBody] PostRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<PostDto>.Error("Datos inválidos"));
            }

            try
            {
                var postDto = new PostDto
                {
                    Texto = request.Texto,
                    PeliculaId = request.PeliculaId
                };
                var post = await _postService.CreatePostAsync(postDto);
                return Ok(ApiResponse<PostDto>.Ok(post));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ApiResponse<PostDto>.Error(ex.Message));
            }
        }
    }
}