using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using CineFans.Common.Requests;
using CineFans.Web.ViewModels;
using System.Net;

namespace CineFans.Web.Controllers
{
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
            var comments = await client.GetFromJsonAsync<List<CommentViewModel>>("comments");
            return View(comments);
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var comment = await client.GetFromJsonAsync<CommentViewModel>($"comments/{id}");

            if (comment == null)
                return NotFound();

            return View(comment);
        }

        public IActionResult Create()
        {
            return View(new CommentViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var request = new CreateCommentRequest
            {
                PostId = model.PostId,
                UserId = model.UserId,
                Text = model.Text,
                Date = model.Date
            };

            var client = _httpClientFactory.CreateClient("CineFansApi");
            var response = await client.PostAsJsonAsync("comments", request);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al crear el comentario.");
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var comment = await client.GetFromJsonAsync<CommentViewModel>($"comments/{id}");

            if (comment == null)
                return NotFound();

            return View(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CommentViewModel model)
        {
            if (id != model.CommentId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            var request = new UpdateCommentRequest
            {
                CommentId = model.CommentId,
                PostId = model.PostId,
                UserId = model.UserId,
                Text = model.Text,
                Date = model.Date
            };

            var client = _httpClientFactory.CreateClient("CineFansApi");
            var response = await client.PutAsJsonAsync("comments", request);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al actualizar el comentario.");
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var comment = await client.GetFromJsonAsync<CommentViewModel>($"comments/{id}");

            if (comment == null)
                return NotFound();

            return View(comment);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var response = await client.DeleteAsync($"comments/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                ModelState.AddModelError("", "El comentario no existe.");
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error al eliminar el comentario.");
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
