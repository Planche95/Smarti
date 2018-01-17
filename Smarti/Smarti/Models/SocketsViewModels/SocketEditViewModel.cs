﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Models.SocketsViewModels
{
    public class SocketEditViewModel
    {
        public int SocketId { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Device Id")]
        [RegularExpression(@"^[0-9]{14}$", ErrorMessage = "Device Id have to contain 14 digits!")]
        public string DeviceId { get; set; }

        [Required]
        [Display(Name = "Room")]
        public int RoomId { get; set; }
        public List<SelectListItem> Rooms { get; set; }
    }
}
