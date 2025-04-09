using CineFansApp.Frontend.ViewModels;
using CineFansApp.Models;
using CineFansApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace CineSocial.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly IMovieService _movieService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IPostService postService,
            IUserService userService,
            IMovieService movieService,
            ILogger<HomeController> logger)
        {
            _postService = postService;
            _userService = userService;
            _movieService = movieService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeVM();

            if (User.Identity.IsAuthenticated)
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                // Obtener publicaciones del feed (de usuarios seguidos y propias)
                viewModel.Posts = await _postService.GetFeedPostsAsync(userId);

                // Obtener usuarios sugeridos (que no sigue el usuario actual)
                viewModel.UsuariosSugeridos = await _userService.GetSuggestedUsersAsync(userId, 5);
            }
            else
            {
                // Para usuarios no autenticados, mostrar publicaciones recientes
                viewModel.Posts = await _postService.GetRecentPostsAsync(10);
            }

            // Obtener películas populares
            viewModel.PeliculasPopulares = await _movieService.GetPopularMoviesAsync(5);

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new CineFansApp.Frontend.ViewModels.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
