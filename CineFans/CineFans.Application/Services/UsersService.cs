using CineFans.Application.Dtos;
using CineFans.Application.Interfaces;
using CineFansApp.Domain.Entities;
using CineFansApp.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;


namespace CineFansApp.Application.Services
{
    public class UserService : IUsersService
    {
        private readonly IUsersRepository _userRepository;

        public UserService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterAsync(UsersRegisterDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                throw new ArgumentException("Passwords do not match.");

            var user = new User
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                FotoPerfil = dto.FotoPerfil,
                FechaRegistro = DateTime.UtcNow
            };

            var addedUser = await _userRepository.AddAsync(user);
            return addedUser != null;
        }

        public async Task<string?> LoginAsync(UsersLoginDto dto)
        {
            var user = await _userRepository.GetUserByEmailAsync(dto.Email);
            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email or password.");

            return GenerateToken(user);
        }

        public async Task<UsersDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with id {id} not found.");

            return MapToDto(user);
        }

        public async Task<IEnumerable<UsersDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(MapToDto);
        }

        public async Task<bool> UpdateProfileAsync(int id, UsersDto dto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with id {id} not found.");

            user.Nombre = dto.Nombre;
            user.Email = dto.Email;
            user.FotoPerfil = dto.FotoPerfil;

            var updatedUser = await _userRepository.UpdateAsync(user);
            return updatedUser != null;
        }

        private UsersDto MapToDto(Users user)
        {
            return new UsersDto
            {
                UserId = user.UserId,
                Nombre = user.Nombre,
                Email = user.Email,
                FotoPerfil = user.FotoPerfil,
                FechaRegistro = user.FechaRegistro,
            };
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create(); // Crea una instancia del algoritmo SHA-256
            var bytes = Encoding.UTF8.GetBytes(password); // Convierte la contraseña en un arreglo de bytes
            var hash = sha256.ComputeHash(bytes); // Genera el hash a partir de los bytes
            return Convert.ToBase64String(hash); // Convierte el hash a una cadena en Base64
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword; // Compara el hash de la contraseña ingresada con el hash almacenado
        }

        private string GenerateToken(Users user)
        {
            // Placeholder for token generation logic  
            return "dummy-token";
        }
    }
}
