using CineFansApp.Application.Interfaces;
using CinesFansApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinesFansApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/authentication/login
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<string>>> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<string>.Error("Credenciales inválidas"));
            }

            try
            {
                var token = await _authService.AuthenticateAsync(request.Email, request.Password);
                return Ok(ApiResponse<string>.Ok(token, "Inicio de sesión exitoso"));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ApiResponse<string>.Error(ex.Message));
            }
        }
    }
}