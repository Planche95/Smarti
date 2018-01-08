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

        public async void Initialize()
        {
            _context.Database.Migrate();

            if (_context.Roles.Any(r => r.Name == "Administrator")) return;

            _roleManager.CreateAsync(new IdentityRole("Administrator")).GetAwaiter().GetResult();

            string user = "me@myemail.com";
            string password = "z0mgchangethis";
            _userManager.CreateAsync(new ApplicationUser { UserName = user, Email = user, EmailConfirmed = true }, password).GetAwaiter().GetResult();
            await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(user), "Administrator");
        }
    }
}
