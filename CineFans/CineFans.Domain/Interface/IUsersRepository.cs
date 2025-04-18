using CineFans.Domain.Entities;

namespace CineFans.Domain.Interface
{
    internal interface IUsersRepository
    {
        Task<Users> GetUserByIdAsync(int userId);
        Task<Users> GetUserByEmailAsync(string email);
        Task<IEnumerable<Users>> GetAllAsync();
        Task<Users> AddAsync(Users user);
        Task<Users> UpdateAsync(Users user);
        Task<bool> DeleteAsync(int userId);
    }
}
