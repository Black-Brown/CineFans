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
            var users = await client.GetFromJsonAsync<List<UsersViewModel>>("users");
            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var user = await client.GetFromJsonAsync<UsersViewModel>($"users/{id}");

            if (user == null)
                return NotFound();

            return View(user);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(UsersViewModel model, IFormFile ProfileImage)
        {
            if (!ModelState.IsValid)
                return View(model);

            string imagePath = string.Empty;

            if (ProfileImage != null && ProfileImage.Length > 0)
            {
                // Genera un nombre único para la imagen
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfileImage.FileName);

                // Ruta física para guardar el archivo
                var filePath = Path.Combine(_environment.WebRootPath, _uploadFolder, fileName);

                // Crear la carpeta si no existe
                var directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(stream);
                }

                // Ruta virtual que se almacenará en la base de datos o se enviará a la API
                imagePath = $"/{_uploadFolder}/{fileName}";
            }

            model.ProfilePicture = imagePath;
            model.RegistrationDate = DateTime.Now;

            var client = _httpClientFactory.CreateClient("CineFansApi");
            var response = await client.PostAsJsonAsync("users", model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al crear el usuario.");
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var user = await client.GetFromJsonAsync<UsersViewModel>($"users/{id}");

            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UsersViewModel model, IFormFile ProfileImage)
        {
            if (id != model.UserId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            // Subir imagen si se incluye una nueva
            if (ProfileImage != null && ProfileImage.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfileImage.FileName);
                var filePath = Path.Combine(_environment.WebRootPath, _uploadFolder, fileName);

                // Asegúrate de que la carpeta exista
                var directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(stream);
                }

                model.ProfilePicture = $"/{_uploadFolder}/{fileName}";
            }

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
            var user = await client.GetFromJsonAsync<UsersViewModel>($"users/{id}");

            if (user == null)
                return NotFound();

            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient("CineFansApi");
            var user = await client.GetFromJsonAsync<UsersViewModel>($"users/{id}");
            if (user == null)
            {
                ModelState.AddModelError("", "El usuario no existe o ya ha sido eliminado.");
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error al eliminar el usuario.");
            return View("Delete", user);
        }
    }
}
