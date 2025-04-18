using CineFansApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CineFansApp.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Aquí irá la lógica de autenticación con AuthService
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Aquí irá la lógica de registro con AuthService
            return RedirectToAction("Login", "Account");
        }
    }
}
