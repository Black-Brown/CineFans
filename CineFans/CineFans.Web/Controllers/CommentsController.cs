using System.Net.Http.Json;
using CineFans.Common.Dtos;
using CineFans.Common.Requests;
using CineFans.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CineFans.Web.Controllers
{
    [Route("Comments/[action]")]
    public class CommentsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CommentsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var comments = await client.GetFromJsonAsync<List<CommentsViewModel>>("comment");
            return View(comments);
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var comment = await client.GetFromJsonAsync<CommentsViewModel>($"comment/{id}");

            if (comment == null)
                return NotFound();

            return View(comment);
        }

        // GET: Comments/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new CommentsViewModel();
            await LoadSelectLists(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommentsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadSelectLists(model);
                return View(model);
            }

            var request = new CreateCommentRequest
            {
                UserId = model.UserId,
                MovieId = model.MovieId,
                Text = model.Text
            };

            var client = _httpClientFactory.CreateClient("CineFansApi");
            var response = await client.PostAsJsonAsync("comment", request);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            // Leer contenido del error desde la respuesta
            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Error al crear el comentario: {errorContent}");

            await LoadSelectLists(model);
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var comment = await client.GetFromJsonAsync<CommentsViewModel>($"comment/{id}");

            if (comment == null)
                return NotFound();

            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CommentsViewModel model)
        {
            if (id != model.CommentId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            var request = new UpdateCommentRequest
            {
                CommentId = model.CommentId,
                Text = model.Text
            };

            var client = _httpClientFactory.CreateClient("CineFansApi");
            var response = await client.PutAsJsonAsync("comment", request);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al editar el comentario.");
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var comment = await client.GetFromJsonAsync<CommentsViewModel>($"comment/{id}");

            if (comment == null)
                return NotFound();

            return View(comment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var response = await client.DeleteAsync($"comment/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al eliminar el comentario.");
            var comment = await client.GetFromJsonAsync<CommentsViewModel>($"comment/{id}");
            return View("Delete", comment);
        }

        private async Task LoadSelectLists(CommentsViewModel model)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");

            var users = await client.GetFromJsonAsync<List<UserDto>>("users");
            var movies = await client.GetFromJsonAsync<List<MovieDto>>("movie");

            model.Users = users?.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();

            model.Movies = movies?.Select(m => new SelectListItem
            {
                Value = m.MovieId.ToString(),
                Text = m.Title
            }).ToList();
        }
    }
}
