using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Models
{
    public class Room
    {
        public int RoomId { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Socket> Sockets { get; set; }

    }
}
