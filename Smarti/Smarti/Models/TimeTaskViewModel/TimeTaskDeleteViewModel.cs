using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Models.TimeTaskViewModel
{
    public class TimeTaskDeleteViewModel
    {
        public int TimeTaskId { get; set; }
        public bool Action { get; set; }
        public DateTime TimeStamp { get; set; }
        public string BackgroundJobId { get; set; }

        public int SocketId { get; set; }
    }
}
