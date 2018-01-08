using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Models
{
    public class SocketData
    {
        public int SocketDataId { get; set; }
        public float Value { get; set; }
        public DateTime TimeStamp { get; set; }

        public int SocketId { get; set; }
        public Socket Socket { get; set; }
    }
}
