using CineFans.Application.Contracts;
using CineFans.Common.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CineFans.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentDto commentDto)
        {
            if (commentDto == null)
                return BadRequest();

            var createdComment = await _commentService.CreateCommentAsync(commentDto);
            return CreatedAtAction(nameof(GetCommentsByMovieId), new { movieId = createdComment.MovieId }, createdComment);
        }

        [HttpGet("movie/{movieId}")]
        public async Task<IActionResult> GetCommentsByMovieId(int movieId)
        {
            var comments = await _commentService.GetCommentsByMovieIdAsync(movieId);
            return Ok(comments);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCommentsByUserId(int userId)
        {
            var comments = await _commentService.GetCommentsByUserIdAsync(userId);
            return Ok(comments);
        }
    }
}
