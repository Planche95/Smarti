using Smarti.Models.SocketsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Models.RoomViewModels
{
    public class RoomListViewModel
    {
        public int RoomId { get; set; }
        public string Name { get; set; }

        public IEnumerable<SocketListViewModel> Sockets { get; set; }
    }
}
