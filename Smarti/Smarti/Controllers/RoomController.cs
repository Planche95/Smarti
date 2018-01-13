using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Smarti.Models;
using Smarti.Services;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Smarti.Models.RoomViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Smarti.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRoomRepository _roomRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public RoomController(UserManager<ApplicationUser> userManager, IRoomRepository roomRepository, IAuthorizationService authorizationService, IMapper mapper)
        {
            _userManager = userManager;
            _roomRepository = roomRepository;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        public IActionResult Create()
        {
            return View(new RoomCreateViewModel());
        }

        [HttpPost]
        public IActionResult Create(RoomCreateViewModel model)
        {
            Room room = _mapper.Map<Room>(model);
            room.UserId = _userManager.GetUserId(User);

            _roomRepository.CreateRoom(room);
            _roomRepository.Savechanges();

            return RedirectToAction("Index", "Socket");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Room room = _roomRepository.GetRoomById(id);

            AuthorizationResult authorizationResult = await _authorizationService
                .AuthorizeAsync(User, room, Operations.Update);

            if (!authorizationResult.Succeeded)
            {
                return new ForbidResult();
            }

            RoomEditViewModel roomEditViewModel = _mapper.Map<RoomEditViewModel>(room);
            
            return View(roomEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(RoomEditViewModel model)
        {
            Room room = _mapper.Map<Room>(model);

            _roomRepository.EditRoom(room);
            _roomRepository.Savechanges();

            return RedirectToAction("Index", "Socket");
        }

        public async Task<IActionResult> Delete(int id)
        {
            Room room = _roomRepository.GetRoomById(id);

            AuthorizationResult authorizationResult = await _authorizationService
                .AuthorizeAsync(User, room, Operations.Delete);

            if (!authorizationResult.Succeeded)
            {
                return new ForbidResult();
            }

            RoomDeleteViewModel roomDeleteViewModel = _mapper.Map<RoomDeleteViewModel>(room);

            return View(roomDeleteViewModel);
        }

        [HttpPost]
        public IActionResult Delete(RoomDeleteViewModel model)
        {
            _roomRepository.DeleteRoom(model.RoomId);
            _roomRepository.Savechanges();

            return RedirectToAction("Index", "Socket");
        }
    }
}