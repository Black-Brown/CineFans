using System.Diagnostics;
using CineFans.Web.Models;
using Microsoft.AspNetCore.Mvc;
using CineFans.Application.Contracts; // ?? Agrega este using para encontrar IMovieService
using System.Threading.Tasks;

namespace CineFans.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService; // ?? Agregado

        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService; // ?? Guardamos el servicio
        }

        public async Task<IActionResult> Index()
        {
            var moviesWithComments = await _movieService.GetMoviesWithCommentsAsync(); // ?? Llamada al servicio
            return View(moviesWithComments); // ?? Lo mandamos a la vista
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
