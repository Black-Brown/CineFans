using CineFans.Application.Contracts;
using CineFans.Common.Dtos;
using CineFans.Common.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CineFans.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // GET: api/Comment
        [HttpGet]
        public async Task<ActionResult<List<CommentDto>>> GetAll()
        {
            var comments = await _commentService.GetAllAsync();
            return Ok(comments);
        }

        // GET: api/Comment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDto>> GetById(int id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
                return NotFound();

            return Ok(comment);
        }

        // POST: api/Comment
        [HttpPost]
        public async Task<ActionResult> Create(CreateCommentRequest request)
        {
            var result = await _commentService.CreateAsync(request);
            if (!result)
                return BadRequest("Error al crear el comentario.");

            return Ok("Comentario creado exitosamente.");
        }

        // PUT: api/Comment
        [HttpPut]
        public async Task<ActionResult> Update(UpdateCommentRequest request)
        {
            var result = await _commentService.UpdateAsync(request);
            if (!result)
                return NotFound("No se pudo actualizar el comentario.");

            return Ok("Comentario actualizado exitosamente.");
        }

        // DELETE: api/Comment/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _commentService.DeleteAsync(id);
            if (!result)
                return NotFound("No se encontró el comentario.");

            return Ok("Comentario eliminado exitosamente.");
        }
    }
}
