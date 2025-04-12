using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineFansApp.Domain.Entities
{
    public class User
    {
        public User()
        {
            Posts = new HashSet<Post>();
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
            Followers = new HashSet<Follow>();
            Following = new HashSet<Follow>();

            Nombre = string.Empty;
            Email = string.Empty;
            PasswordHash = string.Empty;
            FotoPerfil = string.Empty;
        }

        public int UserId { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FotoPerfil { get; set; }
        public DateTime FechaRegistro { get; set; }

        // Navigation properties
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Follow> Followers { get; set; }
        public virtual ICollection<Follow> Following { get; set; }
    }
}
