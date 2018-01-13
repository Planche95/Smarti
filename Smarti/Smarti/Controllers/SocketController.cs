using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Smarti.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Smarti.Services;
using Smarti.Models.SocketsViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smarti.Models.RoomViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Smarti.Controllers
{
    [Authorize]
    public class SocketController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRoomRepository _roomRepository;
        private readonly ISocketRepository _socketRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public SocketController(UserManager<ApplicationUser> userManager, IRoomRepository roomRepository, ISocketRepository socketRepository, IAuthorizationService authorizationService, IMapper mapper)
        {
            _userManager = userManager;
            _roomRepository = roomRepository;
            _socketRepository = socketRepository;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            IEnumerable<Room> rooms =_roomRepository.Rooms
                        .Include(r => r.ApplicationUser)
                        .Where(r => r.ApplicationUser.Id.Equals(_userManager.GetUserId(User)))
                        .Include(r => r.Sockets)
                        .ToList();

            IEnumerable<RoomListViewModel> roomsViewModel = _mapper
                .Map<IEnumerable<Room>, IEnumerable<RoomListViewModel>>(rooms);

            return View(roomsViewModel);
        }

        public async Task<IActionResult> Create(int id)
        {
            Room room =_roomRepository.GetRoomById(id);
            Socket socket = new Socket { Room = room, RoomId = id };

            AuthorizationResult authorizationResult = await _authorizationService
                .AuthorizeAsync(User, socket, Operations.Create);

            if (!authorizationResult.Succeeded)
            {
                return new ForbidResult();
            }

            SocketCreateViewModel socketViewModel = _mapper.Map<SocketCreateViewModel>(socket);

            return View(socketViewModel);
        }

        [HttpPost]
        public IActionResult Create(SocketCreateViewModel model)
        {
            Socket socket = _mapper.Map<Socket>(model);
            _socketRepository.CreateSocket(socket);
            _socketRepository.Savechanges();

            return RedirectToAction("Index", "Socket");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Socket socket = _socketRepository.GetSocketById(id);

            AuthorizationResult authorizationResult = await _authorizationService
                .AuthorizeAsync(User, socket, Operations.Update);

            if (!authorizationResult.Succeeded)
            {
                return new ForbidResult();
            }

            SocketEditViewModel socketViewModel = _mapper.Map<SocketEditViewModel>(socket);
            socketViewModel.Rooms = GetRoomsSelectList();

            return View(socketViewModel);
        }

        [HttpPost]
        public IActionResult Edit(SocketEditViewModel model)
        {
            Socket socket = _mapper.Map<Socket>(model);
            _socketRepository.EditSocket(socket);
            _socketRepository.Savechanges();

            return RedirectToAction("Index", "Socket");
        }

        public async Task<IActionResult> Delete(int id)
        {
            Socket socket = _socketRepository.GetSocketById(id);

            AuthorizationResult authorizationResult = await _authorizationService
                .AuthorizeAsync(User, socket, Operations.Delete);

            if (!authorizationResult.Succeeded)
            {
                return new ForbidResult();
            }

            SocketDeleteViewModel socketViewModel = _mapper.Map<SocketDeleteViewModel>(socket);

            return View(socketViewModel);
        }

        [HttpPost]
        public IActionResult Delete(SocketDeleteViewModel model)
        {
            _socketRepository.DeleteSocket(model.SocketId);
            _socketRepository.Savechanges();

            return RedirectToAction("Index", "Socket");
        }

        #region Helpers

        private List<SelectListItem> GetRoomsSelectList()
        {
            List<SelectListItem> rooms = new List<SelectListItem>();

            foreach (Room room in _roomRepository.Rooms
                                                 .Where(r => r.UserId.Equals(_userManager.GetUserId(User)))
                                                 .ToList())
            {
                rooms.Add(new SelectListItem()
                {
                    Text = room.Name,
                    Value = room.RoomId.ToString()
                });
            }

            return rooms;
        }

        #endregion
    }
}