using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineFansApp.Domain.Entities
{
    public class Like
    {
        public int PublicacionId { get; set; }
        public int UsuarioId { get; set; }

        // Navigation properties
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
