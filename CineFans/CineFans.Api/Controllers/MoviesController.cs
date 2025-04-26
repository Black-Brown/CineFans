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

        // Constructor donde inyectamos el servicio de películas
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // Crear una nueva película
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMovieRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Si el modelo no es válido, devolvemos un error
            }

            // Llamamos al servicio para crear la película
            var response = await _movieService.CreateAsync(model);

            if (response == null)
            {
                return BadRequest("Error al crear la película."); // Manejo de error si la creación no fue exitosa
            }

            // Si la creación fue exitosa, devolvemos el resultado
            return CreatedAtAction(nameof(GetById), new { id = response.MovieId }, response);
        }

        // Obtener una película por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movieResponse = await _movieService.GetByIdAsync(id);

            if (movieResponse == null)
            {
                return NotFound("Película no encontrada.");
            }

            return Ok(movieResponse);
        }

        // Obtener todas las películas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var moviesResponse = await _movieService.GetAllAsync();

            if (moviesResponse == null || !moviesResponse.Any())
            {
                return NotFound("No se encontraron películas.");
            }

            return Ok(moviesResponse);
        }

        // Actualizar una película
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMovieRequest model)
        {
            if (id != model.MovieId)
            {
                return BadRequest("ID de película no coincide.");
            }

            var success = await _movieService.UpdateAsync(model);

            if (!success)
            {
                return NotFound("Película no encontrada.");
            }

            return NoContent(); // Devolvemos un 204 No Content para indicar que la actualización fue exitosa
        }

        // Eliminar una película
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _movieService.DeleteAsync(id);

            if (!success)
            {
                return NotFound("Película no encontrada.");
            }

            return NoContent(); // Devolvemos un 204 No Content para indicar que la eliminación fue exitosa
        }
    }
}
