using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Models.TimeTaskViewModel
{
    public class TimeTaskEditViewModel
    {
        public int TimeTaskId { get; set; }
        public bool Action { get; set; }

        [Required]
        [Display(Name = "Time")]
        public DateTime TimeStamp { get; set; }
        public string BackgroundJobId { get; set; }

        public int SocketId { get; set; }
    }
}
