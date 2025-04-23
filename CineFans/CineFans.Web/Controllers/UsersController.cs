using System.Net.Http.Json;
using CineFans.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CineFans.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _environment;

        public UsersController(IHttpClientFactory httpClientFactory, IWebHostEnvironment environment)
        {
            _httpClientFactory = httpClientFactory;
            _environment = environment;
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

            string imagePath = string.Empty; // Initialize with an empty string to avoid null  

            if (ProfileImage != null && ProfileImage.Length > 0)
            {
                // Generate a unique name for the image  
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfileImage.FileName);

                // Physical path to save the file  
                var filePath = Path.Combine(_environment.WebRootPath, "Images", "profiles", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(stream);
                }

                // Virtual path to save in the database or send to the API  
                imagePath = $"/Images/profiles/{fileName}";
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
                var filePath = Path.Combine(_environment.WebRootPath, "Images", "profiles", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(stream);
                }

                model.ProfilePicture = $"/Images/profiles/{fileName}";
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
            var response = await client.DeleteAsync($"api/Genres/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al eliminar el género.");
            var genre = await client.GetFromJsonAsync<GenreViewModel>($"api/Genres/{id}");
            return View("Delete", genre);
        }
    }
}
