using CinesFansApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CineFansApp.Application.Interfaces;
using CineFansApp.Domain.DTOs;

namespace CinesFansApp.API.Controllers
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

        // POST: api/comments
        [HttpPost]
        public async Task<ActionResult<ApiResponse<CommentDto>>> CreateComment([FromBody] CommentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<CommentDto>.Error("Datos inválidos"));
            }

            try
            {
                var commentDto = new CommentDto
                {
                    PublicacionId = request.PublicacionId,
                    Texto = request.Texto,
                    Fecha = DateTime.UtcNow
                };

                var comment = await _commentService.CreateCommentAsync(commentDto);
                return Ok(ApiResponse<CommentDto?>.Ok(comment));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ApiResponse<CommentDto>.Error(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<CommentDto>.Error(ex.Message));
            }
        }
    }
}