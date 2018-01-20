using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Smarti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            _context.Database.Migrate();

            if (_userManager.Users.Any()) return;

            string user = "tom@smarti.com";
            string password = "Smarti1243";
            _userManager.CreateAsync(new ApplicationUser { UserName = user, Email = user, EmailConfirmed = true }, password).GetAwaiter().GetResult();

            string userId =_userManager.FindByEmailAsync(user).GetAwaiter().GetResult().Id;

            _context.Rooms.AddRange(
                new Room { UserId = userId, Name = "Sypialnia", Sockets = new List<Socket> {
                        new Socket { DeviceId = "12345671234511", Name = "Lampka nocna", SocketDatas = new List<SocketData> {
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 19, 18, 0), Value = 14.0F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 19, 19, 0), Value = 13.8F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 19, 20, 0), Value = 14.1F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 19, 20, 34, 0), Value = 13.0F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 19, 20, 35, 0), Value = 15.8F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 19, 20, 36, 0), Value = 13.1F }
                            } }
                    } },
                new Room { UserId = userId, Name = "Salon", Sockets = new List<Socket> {
                        new Socket { DeviceId = "12345671234512", Name = "Konsola", SocketDatas = new List<SocketData> {
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 21, 00, 0), Value = 46.8F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 21, 01, 0), Value = 50.2F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 21, 02, 0), Value = 49.5F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 20, 7, 28, 0), Value = 48.8F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 20, 7, 29, 0), Value = 51.2F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 20, 7, 30, 0), Value = 47.5F }
                            } },
                        new Socket { DeviceId = "12345671234513", Name = "Komoda", SocketDatas = new List<SocketData> {
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 16, 23, 0), Value = 19.7F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 16, 24, 0), Value = 19.8F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 16, 25, 0), Value = 19.6F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 17, 16, 30, 0), Value = 20.7F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 17, 16, 31, 0), Value = 21.8F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 17, 16, 32, 0), Value = 22.6F }
                            } },
                        new Socket { DeviceId = "12345671234514", Name = "Lampka", SocketDatas = new List<SocketData> {
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 16, 23, 0), Value = 40.0F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 16, 24, 0), Value = 40.1F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 16, 25, 0), Value = 40.0F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 19, 18, 0), Value = 40.0F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 19, 19, 0), Value = 40.1F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 19, 20, 0), Value = 40.0F }
                            } }
                    }
                },
                new Room { UserId = userId, Name = "Kuchnia", Sockets = new List<Socket> {
                        new Socket { DeviceId = "12345671234515", Name = "Barek", SocketDatas = new List<SocketData> {
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 14, 57, 0), Value = 31.0F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 14, 58, 0), Value = 32.2F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 14, 59, 0), Value = 30.7F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 16, 14, 46, 0), Value = 33.0F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 16, 14, 47, 0), Value = 31.2F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 16, 14, 48, 0), Value = 29.7F }
                            } },
                        new Socket { DeviceId = "12345671234516", Name = "Ledy", SocketDatas = new List<SocketData> {
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 14, 18, 0), Value = 43.3F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 14, 19, 0), Value = 46.7F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 21, 14, 20, 0), Value = 45.0F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 14, 14, 50, 0), Value = 45.3F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 14, 14, 51, 0), Value = 45.7F },
                                new SocketData { TimeStamp = new DateTime(2018, 01, 14, 14, 52, 0), Value = 45.0F }
                            } }
                    }
                } );

            _context.SaveChanges();
        }
    }
}
