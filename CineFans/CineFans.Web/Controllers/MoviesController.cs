using System.Net.Http.Json;
using CineFans.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using CineFans.Common.Responses;

namespace CineFans.Web.Controllers
{
    [Route("Movies/[action]")]
    public class MoviesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _environment;
        private readonly string _uploadFolder;

        // Inyectamos IConfiguration para acceder a la configuración de la carpeta de subida
        public MoviesController(IHttpClientFactory httpClientFactory, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _environment = environment;
            _uploadFolder = configuration["FileStorage:MovieUploadFolder"] ?? throw new ArgumentNullException(nameof(configuration), "Upload folder configuration is missing.");
        }

        // Acción para obtener todas las películas con comentarios
        public async Task<IActionResult> MoviesWithComments()
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var moviesWithComments = await client.GetFromJsonAsync<List<MovieWithCommentsViewModel>>("Movie/movies-with-comments"); // Llamamos a la API para obtener las películas con comentarios

            if (moviesWithComments == null)
                return NotFound();

            return View(moviesWithComments); // Pasamos los datos a la vista
        }

        // Obtener todas las películas
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var movies = await client.GetFromJsonAsync<List<MovieViewModel>>("movie");
            return View(movies);
        }

        // Obtener detalles de una película
        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            try
            {
                var movie = await client.GetFromJsonAsync<MovieViewModel>($"Movie/{id}"); // Ajustado a la ruta de la API
                if (movie == null) return NotFound();
                return View(movie);
            }
            catch
            {
                return NotFound();
            }
        }

        // Crear una nueva película
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieViewModel model, IFormFile ImageFile)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Guardar la imagen en la carpeta wwwroot/uploads
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

                try
                {
                    var directoryPath = Path.GetDirectoryName(filePath);
                    if (directoryPath != null && !Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    model.ImageUrl = $"/uploads/{fileName}";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error al subir la imagen: {ex.Message}");
                    return View(model);
                }
            }

            // Enviar la nueva película a la API
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var response = await client.PostAsJsonAsync("movie", model); // Ajustado a la ruta de la API

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al crear la película.");
            return View(model);
        }

        // Editar una película existente
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            try
            {
                var movie = await client.GetFromJsonAsync<MovieViewModel>($"Movie/{id}"); // Ajustado a la ruta de la API
                if (movie == null)
                    return NotFound();

                return View(movie);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieViewModel model)
        {
            if (id != model.MovieId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

                try
                {
                    var directoryPath = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);

                        if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                    }

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await model.ImageFile.CopyToAsync(stream);
                    model.ImageUrl = $"/uploads/{fileName}";
                }
                catch
                {
                    ModelState.AddModelError("", "Error al subir la imagen.");
                    return View(model);
                }
            }

            var client = _httpClientFactory.CreateClient("CineFansApi");
            var response = await client.PutAsJsonAsync($"Movie/{id}", model); // Ajustado a la ruta de la API

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al editar la película.");
            return View(model);
        }

        // Eliminar una película
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            try
            {
                var movie = await client.GetFromJsonAsync<MovieViewModel>($"Movie/{id}"); // Ajustado a la ruta de la API
                if (movie == null)
                    return NotFound();

                return View(movie);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var response = await client.DeleteAsync($"Movie/{id}"); // Ajustado a la ruta de la API

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "No se pudo eliminar la película.");
            return RedirectToAction(nameof(Delete), new { id });
        }

    }
}
