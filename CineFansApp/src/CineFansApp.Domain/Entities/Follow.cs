using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineFansApp.Domain.Entities
{
    public class Follow
    {
        public int SeguidorId { get; set; }
        public int SeguidoId { get; set; }

        // Navigation properties
        public virtual User Follower { get; set; }
        public virtual User Following { get; set; }

    }
}
