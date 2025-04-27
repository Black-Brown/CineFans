using System.Net.Http.Json;
using CineFans.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CineFans.Web.Controllers
{
    [Route("Users/[action]")]
    public class UsersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _environment;
        private readonly string _uploadFolder;

        public UsersController(IHttpClientFactory httpClientFactory, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _environment = environment;
            _uploadFolder = configuration["FileStorage:UserUploadFolder"] ?? "images/profiles";
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var users = await client.GetFromJsonAsync<List<UserViewModel>>("users");
            return View(users ?? new List<UserViewModel>());
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var user = await client.GetFromJsonAsync<UserViewModel>($"users/{id}");

            if (user == null)
                return NotFound();

            return View(user);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                // Aquí puedes agregar un registro de log o inspeccionar el estado del modelo
                return View(model);
            }

            // Asignamos la fecha de registro
            model.RegistrationDate = DateTime.UtcNow;

            // Crear cliente HTTP
            var client = _httpClientFactory.CreateClient("CineFansApi");

            // Enviar solicitud POST para crear el usuario
            var response = await client.PostAsJsonAsync("users", model);

            // Verificar si la respuesta fue exitosa
            if (response.IsSuccessStatusCode)
            {
                // Redirigir al índice o a la página de éxito si la creación fue exitosa
                return RedirectToAction(nameof(Index));
            }

            // Si la respuesta no fue exitosa, capturamos detalles del error
            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Error al crear el usuario. Detalles: {errorContent}");

            // En caso de error, devolver la vista con el mensaje de error
            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var user = await client.GetFromJsonAsync<UserViewModel>($"users/{id}");

            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserViewModel model)
        {
            if (id != model.UserId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient("CineFansApi");
            var response = await client.PutAsJsonAsync($"users/{id}", model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al actualizar el usuario.");
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var user = await client.GetFromJsonAsync<UserViewModel>($"users/{id}");

            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var response = await client.DeleteAsync($"users/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al eliminar el usuario.");
            var user = await client.GetFromJsonAsync<UserViewModel>($"users/{id}");
            return View("Delete", user);
        }
    }
}
