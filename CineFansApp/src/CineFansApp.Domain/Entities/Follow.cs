using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineFansApp.Domain.Entities
{
    internal class Follow
    {
        public int SeguidorId { get; set; }
        public int SeguidoId { get; set; }

        // Navigation properties
        public User Follower { get; set; }
        public User Following { get; set; }
    }
}
