using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Smarti.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Smarti.Services;

namespace Smarti.Controllers
{
    public class SocketController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRoomRepository _roomRepository;
        private readonly ISocketRepository _socketRepository;

        public SocketController(UserManager<ApplicationUser> userManager, IRoomRepository roomRepository, ISocketRepository socketRepository)
        {
            _userManager = userManager;
            _roomRepository = roomRepository;
            _socketRepository = socketRepository;
        }

        public IActionResult Index()
        {
            return View
                (
                    _roomRepository.Rooms
                        .Include(r => r.ApplicationUser)
                        .Where(r => r.ApplicationUser.Id.Equals(_userManager.GetUserId(User)))
                        .Include(r => r.Sockets)
                        .ToList()
                );
        }

        public IActionResult Create(int id)
        {
            return View(new Socket { RoomId = id });
        }

        [HttpPost]
        public IActionResult Create(Socket model)
        {
            _socketRepository.CreateSocket(model);
            _socketRepository.Savechanges();

            return RedirectToAction("Index", "Socket");
        }

        public IActionResult Edit(int id)
        {
            return View(_socketRepository.GetSocketById(id));
        }

        [HttpPost]
        public IActionResult Edit(Socket model)
        {
            _socketRepository.EditSocket(model);
            _socketRepository.Savechanges();

            return RedirectToAction("Index", "Socket");
        }

        public IActionResult Delete(int id)
        {
            return View(_socketRepository.GetSocketById(id));
        }

        [HttpPost]
        public IActionResult Delete(Socket model)
        {
            _socketRepository.DeleteSocket(model.SocketId);
            _socketRepository.Savechanges();

            return RedirectToAction("Index", "Socket");
        }
    }
}