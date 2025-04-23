using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineFans.Domain.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "NVARCHAR(100)")]
        public string Email { get; set; } = string.Empty;

        [Column(TypeName = "NVARCHAR(255)")]
        public string PasswordHash { get; set; } = string.Empty;

        [Column(TypeName = "NVARCHAR(255)")]
        public string ProfilePicture { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }

        // Propiedades de navegación
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Follower> Followers { get; set; } = new List<Follower>();
        public ICollection<Follower> Following { get; set; } = new List<Follower>();
    }
}
