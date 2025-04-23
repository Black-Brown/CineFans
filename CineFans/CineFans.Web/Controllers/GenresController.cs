using System.Net.Http.Json;
using CineFans.Common.Requests;
using Microsoft.AspNetCore.Mvc;
using CineFans.Web.ViewModels;
using System.Net;

namespace CineFans.Web.Controllers
{
    public class GenresController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GenresController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var genres = await client.GetFromJsonAsync<List<GenreViewModel>>("genres");
            return View(genres);
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var genre = await client.GetFromJsonAsync<GenreViewModel>($"genres/{id}");

            if (genre == null)
                return NotFound();

            return View(genre);
        }

        public IActionResult Create()
        {
            return View(new GenreViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenreViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var request = new CreateGenreRequest
            {
                Name = model.Name
            };

            var client = _httpClientFactory.CreateClient("CineFansApi");
            var response = await client.PostAsJsonAsync("genres", request);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al crear el género.");
            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var genre = await client.GetFromJsonAsync<GenreViewModel>($"genres/{id}");

            if (genre == null)
                return NotFound();

            return View(genre); 
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, GenreViewModel model)
        {
            if (id != model.GenreId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            var updateRequest = new UpdateGenreRequest
            {
                Id = model.GenreId,
                Name = model.Name
            };

            var client = _httpClientFactory.CreateClient("CineFansApi");
            var response = await client.PutAsJsonAsync("genres", updateRequest);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al editar el género.");
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var genre = await client.GetFromJsonAsync<GenreViewModel>($"genres/{id}");

            if (genre == null)
                return NotFound();

            return View(genre);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var response = await client.DeleteAsync($"Genres/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            // Manejo específico para "No encontrado"
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                ModelState.AddModelError("", "El género no existe.");
                return RedirectToAction(nameof(Index));
            }

            // Manejo de otros errores
            ModelState.AddModelError("", "Error al eliminar el género.");
            return RedirectToAction(nameof(Delete), new { id });
        }

    }
}
