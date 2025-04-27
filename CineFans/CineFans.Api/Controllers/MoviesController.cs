using CineFans.Application.Contracts;
using CineFans.Common.Requests;
using CineFans.Common.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CineFans.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // Crear una nueva película
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMovieRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _movieService.CreateAsync(model);

            if (response == null)
                return StatusCode(500, "Error interno al crear la película.");

            return CreatedAtAction(nameof(GetById), new { id = response.MovieId }, response);
        }

        // Obtener una película por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieResponse>> GetById(int id)
        {
            var movieResponse = await _movieService.GetByIdAsync(id);

            if (movieResponse == null)
                return NotFound(new { message = "Película no encontrada." });

            return Ok(movieResponse);
        }

        // Obtener todas las películas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieResponse>>> GetAll()
        {
            var moviesResponse = await _movieService.GetAllAsync();

            if (moviesResponse == null || !moviesResponse.Any())
                return Ok(new List<MovieResponse>()); // Mejor devolver lista vacía

            return Ok(moviesResponse);
        }

        // Actualizar una película
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMovieRequest model)
        {
            if (id != model.MovieId)
                return BadRequest(new { message = "El ID de la película no coincide con el de la solicitud." });

            var success = await _movieService.UpdateAsync(model);

            if (!success)
                return NotFound(new { message = "Película no encontrada para actualizar." });

            return NoContent();
        }

        // Eliminar una película
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _movieService.DeleteAsync(id);

            if (!success)
                return NotFound(new { message = "Película no encontrada para eliminar." });

            return NoContent();
        }
    }
}
