using CineFansApp.Application.Interfaces;
using CineFansApp.Frontend.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CineSocial.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;

        public ProfileController(IUserService userService, IPostService postService)
        {
            _userService = userService;
            _postService = postService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var posts = await _postService.GetPostsByUserIdAsync(id);

            var viewModel = new ProfileVM
            {
                User = user,
                Posts = posts,
                IsCurrentUser = false,
                IsFollowing = false
            };

            if (User.Identity?.IsAuthenticated == true)
            {
                int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                viewModel.IsCurrentUser = currentUserId == id;

                if (!viewModel.IsCurrentUser)
                {
                    viewModel.IsFollowing = await _userService.IsFollowingAsync(currentUserId, id);
                }
            }

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Follow(int id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int currentUserId = int.Parse(userIdClaim);
            await _userService.FollowUserAsync(currentUserId, id);
            return RedirectToAction(nameof(Index), new { id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Unfollow(int id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int currentUserId = int.Parse(userIdClaim);
            await _userService.UnfollowUserAsync(currentUserId, id);
            return RedirectToAction(nameof(Index), new { id });
        }

        [Authorize]
        public async Task<IActionResult> Followers(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var followers = await _userService.GetFollowersAsync(id);
            ViewBag.User = user;
            return View(followers);
        }

        [Authorize]
        public async Task<IActionResult> Following(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var following = await _userService.GetFollowingAsync(id);
            ViewBag.User = user;
            return View(following);
        }
    }
}
