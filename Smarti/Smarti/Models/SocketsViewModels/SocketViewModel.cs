using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Models.SocketsViewModels
{
    public class SocketViewModel
    {
        public int SocketId { get; set; }
        public string Name { get; set; }
        public string DeviceId { get; set; }

        public Room Room { get; set; }
    }
}
