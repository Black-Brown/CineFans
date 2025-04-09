using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineFansApp.Domain.Entities
{
    internal class Like
    {
        public int PublicacionId { get; set; }
        public int UsuarioId { get; set; }

        // Navigation properties
        public Post Post { get; set; }
        public User User { get; set; }
    }
}
