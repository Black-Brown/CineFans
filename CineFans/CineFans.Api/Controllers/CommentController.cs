using CineFans.Application.Contracts;
using CineFans.Common.Dtos;
using CineFans.Common.Requests;
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
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequest request)
        {
            if (request == null)
                return BadRequest();

            var commentDto = new CommentDto
            {
                Text = request.Text,
                MovieId = request.MovieId,
                UserId = request.UserId,
                Date = DateTime.Now // Puedes setear la fecha aquí si quieres
            };

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
        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentService.GetAllCommentsAsync();
            return Ok(comments);
        }

        [HttpGet("{commentId}")]
        public async Task<IActionResult> GetCommentById(int commentId)
        {
            var comment = await _commentService.GetCommentByIdAsync(commentId);
            if (comment == null)
                return NotFound();

            return Ok(comment);
        }
    }
}
