using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Models.SocketsViewModels
{
    public class SocketCreateViewModel
    {
        public int SocketId { get; set; }
        public string Name { get; set; }
        public string DeviceId { get; set; }

        public int RoomId { get; set; }
    }
}
