using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Smarti.Models
{
    public class ApplicationUser : IdentityUser
    {
        //For now lazy loading is not supported (it will from 2.1v), but virtual can be added
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
