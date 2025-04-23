using CineFans.Application.Contracts;
using CineFans.Common.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CineFans.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }
      
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _genreService.GetAllAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var genre = await _genreService.GetByIdAsync(id);
            if (genre == null) return NotFound();
            return Ok(genre);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGenreRequest request)
        {
            await _genreService.AddAsync(request);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateGenreRequest request)
        {
            await _genreService.UpdateAsync(request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _genreService.DeleteAsync(id);
            return Ok();
        }
    }
}
