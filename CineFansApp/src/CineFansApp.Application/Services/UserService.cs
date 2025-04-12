using CineFansApp.Application.Interfaces;
using CineFansApp.Domain.DTOs;
using CineFansApp.Domain.Entities;
using CineFansApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineFansApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"Usuario con id {userId} no encontrado");

            return MapToDto(user);
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                throw new KeyNotFoundException($"Usuario con email {email} no encontrado");

            return MapToDto(user);
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(MapToDto).ToList();
        }

        public async Task<List<UserDto>> GetSuggestedUsersAsync(int userId, int count)
        {
            var users = await _userRepository.GetSuggestedUsersAsync(userId, count);
            return users.Select(MapToDto).ToList();
        }

        public async Task<List<UserDto>> GetFollowersAsync(int userId)
        {
            var followers = await _userRepository.GetFollowersAsync(userId);
            return followers.Select(MapToDto).ToList();
        }

        public async Task<List<UserDto>> GetFollowingAsync(int userId)
        {
            var following = await _userRepository.GetFollowingAsync(userId);
            return following.Select(MapToDto).ToList();
        }

        public async Task<bool> IsFollowingAsync(int followerId, int followedId)
        {
            return await _userRepository.IsFollowingAsync(followerId, followedId);
        }

        public async Task FollowUserAsync(int followerId, int followedId)
        {
            if (followerId == followedId)
                throw new InvalidOperationException("Un usuario no puede seguirse a sí mismo");

            if (await IsFollowingAsync(followerId, followedId))
                return;

            await _userRepository.FollowUserAsync(followerId, followedId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UnfollowUserAsync(int followerId, int followedId)
        {
            if (!await IsFollowingAsync(followerId, followedId))
                return;

            await _userRepository.UnfollowUserAsync(followerId, followedId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UserDto?> UpdateUserAsync(UserDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(userDto.UserId);
            if (user == null)
                throw new KeyNotFoundException($"Usuario con ID {userDto.UserId} no encontrado");

<<<<<<< Updated upstream
            user.Nombre = userDto.Nombre ?? user.Nombre;
            user.FotoPerfil = userDto.FotoPerfil ?? user.FotoPerfil;
=======
            user.Nombre = userDto.Nombre ?? string.Empty;
            user.FotoPerfil = userDto.FotoPerfil ?? string.Empty;
>>>>>>> Stashed changes

            await _unitOfWork.SaveChangesAsync();
            return MapToDto(user);
        }

        private UserDto MapToDto(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                Nombre = user.Nombre,
                Email = user.Email,
                FotoPerfil = user.FotoPerfil,
                FechaRegistro = user.FechaRegistro,
                SeguidoresCount = user.Followers?.Count ?? 0,
                SiguiendoCount = user.Following?.Count ?? 0,
                PublicacionesCount = user.Posts?.Count ?? 0
            };
        }
    }
}
