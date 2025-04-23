using System.Net.Http.Json;
using CineFans.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CineFans.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _environment;

        public MoviesController(IHttpClientFactory httpClientFactory, IWebHostEnvironment environment)
        {
            _httpClientFactory = httpClientFactory;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var movies = await client.GetFromJsonAsync<List<MovieViewModel>>("movies");
            return View(movies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            try
            {
                var movie = await client.GetFromJsonAsync<MovieViewModel>($"movies/{id}");
                if (movie == null) return NotFound();
                return View(movie);
            }
            catch
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Create()
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var genres = await client.GetFromJsonAsync<List<GenreViewModel>>("genres");

            ViewBag.Genres = new SelectList(genres, "GenreId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await CargarGenerosEnViewBag();
                return View(model);
            }

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                model.ImageUrl = $"/uploads/{fileName}";
            }

            var form = CrearMultipartFormData(model);

            var client = _httpClientFactory.CreateClient("CineFansApi");
            var response = await client.PostAsync("movies", form);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al crear la película.");
            await CargarGenerosEnViewBag();
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");

            try
            {
                var movie = await client.GetFromJsonAsync<MovieViewModel>($"movies/{id}");
                var genres = await client.GetFromJsonAsync<List<GenreViewModel>>("genres");

                if (movie == null)
                    return NotFound();

                ViewBag.Genres = new SelectList(genres, "GenreId", "Name", movie.GenreId);
                return View(movie);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MovieViewModel model)
        {
            if (id != model.MovieId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                await CargarGenerosEnViewBag(model.GenreId);
                return View(model);
            }

            var form = CrearMultipartFormData(model, true);

            var client = _httpClientFactory.CreateClient("CineFansApi");
            var response = await client.PutAsync($"movies/{id}", form);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al editar la película.");
            await CargarGenerosEnViewBag(model.GenreId);
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");

            try
            {
                var movie = await client.GetFromJsonAsync<MovieViewModel>($"movies/{id}");
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
            var response = await client.DeleteAsync($"movies/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "No se pudo eliminar la película.");
            return RedirectToAction(nameof(Delete), new { id });
        }

        // Función auxiliar para evitar repetir código
        private MultipartFormDataContent CrearMultipartFormData(MovieViewModel model, bool incluirId = false)
        {
            var form = new MultipartFormDataContent();

            if (incluirId)
                form.Add(new StringContent(model.MovieId.ToString()), "MovieId");

            form.Add(new StringContent(model.Title ?? ""), "Title");
            form.Add(new StringContent(model.Description ?? ""), "Description");
            form.Add(new StringContent(model.Year.ToString()), "Year");
            form.Add(new StringContent(model.Director ?? ""), "Director");
            form.Add(new StringContent(model.GenreId.ToString()), "GenreId");

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                form.Add(new StreamContent(model.ImageFile.OpenReadStream()), "ImageFile", model.ImageFile.FileName);
            }

            return form;
        }

        private async Task CargarGenerosEnViewBag(int? selectedGenreId = null)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var genres = await client.GetFromJsonAsync<List<GenreViewModel>>("genres");
            ViewBag.Genres = new SelectList(genres, "GenreId", "Name", selectedGenreId);
        }
    }
}
