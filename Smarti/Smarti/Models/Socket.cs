using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Models
{
    public class Socket
    {
        public int SocketId { get; set; }
        public string Name { get; set; }
        public string DeviceId { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public virtual ICollection<SocketData> SocketDatas { get; set; }
    }
}
