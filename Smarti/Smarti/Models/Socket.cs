using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Models
{
    public class Socket
    {
        public int SocketId { get; set; }

        [MaxLength(15)]
        public string Name { get; set; }

        [MaxLength(8)]
        public string DeviceId { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public virtual ICollection<SocketData> SocketDatas { get; set; }
    }
}
