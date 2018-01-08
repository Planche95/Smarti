using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Socket> Sockets { get; set; }

    }
}
