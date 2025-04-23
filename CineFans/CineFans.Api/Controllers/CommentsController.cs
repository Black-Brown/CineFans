using CineFans.Application.Contracts;
using CineFans.Common.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CineFans.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _commentService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
                return NotFound();

            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequest request)
        {
            var response = await _commentService.CreateAsync(request);
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCommentRequest request)
        {
            var response = await _commentService.UpdateAsync(request);
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _commentService.DeleteAsync(id);
            if (!response.Success)
                return NotFound(response.Message);

            return Ok(response.Message);
        }
    }
}
