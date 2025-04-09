using CineFansApp.Application.Interfaces;
using CineFansApp.Frontend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CineFansApp.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IPostService _postService;

        public MovieController(IMovieService movieService, IPostService postService)
        {
            _movieService = movieService;
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return View(movies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            
            var posts = await _postService.GetPostsByMovieIdAsync(id);
            
            var viewModel = new MovieDetailsVM
            {
                Movie = movie,
                Posts = posts
            };
            
            return View(viewModel);
        }
    }
}
