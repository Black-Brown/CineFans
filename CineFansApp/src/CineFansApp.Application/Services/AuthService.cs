using CineFansApp.Application.Interfaces;
using CineFansApp.Domain.DTOs;
using CineFansApp.Domain.Entities;
using CineFansApp.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;
using BCrypt.Net;

namespace CineFansApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<UserDto>> ValidateUserAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return ServiceResult<UserDto>.Error("Email y contraseña son requeridos");

            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                return ServiceResult<UserDto>.Error("Usuario no encontrado");

            bool validPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!validPassword)
                return ServiceResult<UserDto>.Error("Contraseña incorrecta");

            var userDto = new UserDto
            {
                UserId = user.UserId,
                Nombre = user.Nombre,
                Email = user.Email,
                FotoPerfil = user.FotoPerfil,
                FechaRegistro = user.FechaRegistro
            };

            return ServiceResult<UserDto>.Ok(userDto, "Inicio de sesión exitoso");
        }

        public async Task<ServiceResult<UserDto>> RegisterUserAsync(UserDto userDto, string password)
        {
            if (string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(userDto.Nombre))
                return ServiceResult<UserDto>.Error("Nombre, email y contraseña son requeridos");

            if (await _userRepository.GetByEmailAsync(userDto.Email) != null)
                return ServiceResult<UserDto>.Error("El email ya está registrado");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Nombre = userDto.Nombre,
                Email = userDto.Email,
                PasswordHash = passwordHash,
                FotoPerfil = userDto.FotoPerfil ?? "/images/users/default.jpg",
                FechaRegistro = DateTime.Now
            };

            _userRepository.Add(user);
            await _unitOfWork.SaveChangesAsync();

            userDto.UserId = user.UserId;
            userDto.FechaRegistro = user.FechaRegistro;

            return ServiceResult<UserDto>.Ok(userDto, "Registro exitoso");
        }
    }
}
