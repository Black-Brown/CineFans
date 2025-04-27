using System.Net.Http.Json;
using CineFans.Common.Dtos;
using CineFans.Common.Requests.Comment;
using CineFans.Domain.Entities;
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
            var comments = await client.GetFromJsonAsync<List<CommentsViewModel>>($"comment/movie/");
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
            if (ModelState.IsValid)
            {
                // Convertir el UserId de string a int  
                if (!int.TryParse(model.UserId.ToString(), out var userId))
                {
                    ModelState.AddModelError("", "El UserId no es válido.");
                    await LoadSelectLists(model);
                    return View(model);
                }

                var comment = new Comment
                {
                    Text = model.Text,
                    MovieId = model.MovieId,
                    UserId = userId, // Asignado después de la conversión  
                };

                // Enviar el comentario a la API  
                var client = _httpClientFactory.CreateClient("CineFansApi");
                var response = await client.PostAsJsonAsync("comment", comment);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Details", "Movies", new { id = model.MovieId });
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Error al crear el comentario: {errorContent}");
                }
            }

            // Si el modelo no es válido, recargar los SelectLists  
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
                return BadRequest();

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

            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Error al editar el comentario: {errorContent}");

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

            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Error al eliminar el comentario: {errorContent}");

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
                Value = u.UserId.ToString(),
                Text = u.Name
            }).ToList();

            model.Movies = movies?.Select(m => new SelectListItem
            {
                Value = m.MovieId.ToString(),
                Text = m.Title
            }).ToList();
        }

        // NUEVOS MÉTODOS 🎯

        public async Task<IActionResult> GetCommentsByMovie(int movieId)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var comments = await client.GetFromJsonAsync<List<CommentsViewModel>>($"comment/movie/{movieId}");

            if (comments == null || comments.Count == 0)
                return NotFound($"No comments found for movie ID {movieId}.");

            return View("Index", comments);
        }

        public async Task<IActionResult> GetCommentsByUser(int userId)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var comments = await client.GetFromJsonAsync<List<CommentsViewModel>>($"comment/user/{userId}");

            if (comments == null || comments.Count == 0)
                return NotFound($"No comments found for user ID {userId}.");

            return View("Index", comments);
        }
    }
}
