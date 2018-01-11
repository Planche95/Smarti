using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Smarti.Models;

namespace Smarti.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Socket> Sockets { get; set; }
        public DbSet<SocketData> SocketDatas { get; set; }
        public DbSet<TimeTask> TimeTasks { get; set; }
    }
}
