using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Models.RoomViewModels
{
    public class RoomEditViewModel
    {
        public int RoomId { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
