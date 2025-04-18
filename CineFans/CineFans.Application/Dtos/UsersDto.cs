using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineFans.Application.Dtos
{
    internal class UsersDto
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

