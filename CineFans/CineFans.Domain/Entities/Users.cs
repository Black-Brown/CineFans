using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineFans.Domain.Entities
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
    }
}
