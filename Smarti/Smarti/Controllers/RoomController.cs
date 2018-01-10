using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Smarti.Models;
using Smarti.Services;
using Microsoft.AspNetCore.Identity;

namespace Smarti.Controllers
{
    public class RoomController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRoomRepository _roomRepository;

        public RoomController(UserManager<ApplicationUser> userManager, IRoomRepository roomRepository)
        {
            _userManager = userManager;
            _roomRepository = roomRepository;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Room model)
        {
            model.UserId = _userManager.GetUserId(User);
            _roomRepository.CreateRoom(model);
            _roomRepository.Savechanges();

            return RedirectToAction("Index", "Socket");
        }

        public IActionResult Edit(int id)
        {
            return View(_roomRepository.GetRoomById(id));
        }

        [HttpPost]
        public IActionResult Edit(Room model)
        {
            _roomRepository.EditRoom(model);
            _roomRepository.Savechanges();

            return RedirectToAction("Index", "Socket");
        }

        public IActionResult Delete(int id)
        {
            return View(_roomRepository.GetRoomById(id));
        }

        [HttpPost]
        public IActionResult Delete(Room model)
        {
            _roomRepository.DeleteRoom(model.RoomId);
            _roomRepository.Savechanges();

            return RedirectToAction("Index", "Socket");
        }
    }
}