using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Models.TimeTaskViewModel
{
    public class TimeTaskEditViewModel
    {
        public int TimeTaskId { get; set; }
        public bool Type { get; set; }
        public DateTime TimeStamp { get; set; }

        public int SocketId { get; set; }
    }
}
