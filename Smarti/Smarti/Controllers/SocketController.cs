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
using Hangfire;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;

namespace Smarti.Controllers
{
    [Authorize]
    public class SocketController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRoomRepository _roomRepository;
        private readonly ISocketRepository _socketRepository;
        private readonly ITimeTaskRepository _timeTaskRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMqttAppClient _mqttAppClient;
        private readonly IMapper _mapper;

        Dictionary<string, bool?> result;

        public SocketController(UserManager<ApplicationUser> userManager, IRoomRepository roomRepository, ISocketRepository socketRepository, ITimeTaskRepository timeTaskRepository, IAuthorizationService authorizationService, IMqttAppClient mqttAppClient, IMapper mapper)
        {
            _userManager = userManager;
            _roomRepository = roomRepository;
            _socketRepository = socketRepository;
            _timeTaskRepository = timeTaskRepository;
            _authorizationService = authorizationService;
            _mqttAppClient = mqttAppClient;
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

            if (room == null)
            {
                return new NotFoundResult();
            }

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
            bool isDeviceIdExist = _socketRepository.Sockets.Any(s => s.DeviceId == model.DeviceId);

            if (isDeviceIdExist)
            {
                ModelState.AddModelError("", "This device id is already asigned!");
                return View(model);
            }

            result = new Dictionary<string, bool?>
            {
                { model.DeviceId, null }
            };

            _mqttAppClient.SubscribeToMany(new string[] { "sockets/" + model.DeviceId });
            _mqttAppClient.Client.MqttMsgPublishReceived += AckReceived;
            _mqttAppClient.Publish(model.DeviceId, "Check");

            System.Threading.Thread.Sleep(1000);

            if(result[model.DeviceId] == null)
            {
                ModelState.AddModelError("", "Failed connection attempt, make shure that socket is connected!");
                return View(model);
            }

            Socket socket = _mapper.Map<Socket>(model);
            _socketRepository.CreateSocket(socket);
            _socketRepository.Savechanges();

            return RedirectToAction("Index", "Socket");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Socket socket = _socketRepository.GetSocketById(id);

            if (socket == null)
            {
                return new NotFoundResult();
            }

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
            bool isDeviceIdExist = _socketRepository.Sockets.Any(s => s.DeviceId == model.DeviceId 
                                                                    && s.SocketId != model.SocketId);

            if (isDeviceIdExist)
            {
                ModelState.AddModelError("", "This device id is already asigned!");
                return View(model);
            }

            result = new Dictionary<string, bool?>
            {
                { model.DeviceId, null }
            };

            _mqttAppClient.SubscribeToMany(new string[] { "sockets/" + model.DeviceId });
            _mqttAppClient.Client.MqttMsgPublishReceived += AckReceived;
            _mqttAppClient.Publish(model.DeviceId, "Check");

            System.Threading.Thread.Sleep(1000);

            if (result[model.DeviceId] == null)
            {
                ModelState.AddModelError("", "Failed connection attempt, make shure that socket is connected!");
                return View(model);
            }

            Socket socket = _mapper.Map<Socket>(model);
            _socketRepository.EditSocket(socket);
            _socketRepository.Savechanges();

            return RedirectToAction("Index", "Socket");
        }

        public async Task<IActionResult> Delete(int id)
        {
            Socket socket = _socketRepository.GetSocketById(id);

            if (socket == null)
            {
                return new NotFoundResult();
            }

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
            List<TimeTask> timeTasks = _timeTaskRepository.TimeTasks
                                            .Include(tt => tt.Socket)
                                            .Where(tt => tt.Socket.SocketId == model.SocketId)
                                            .ToList();

            foreach (TimeTask timeTask in timeTasks)
            {
                BackgroundJob.Delete(timeTask.BackgroundJobId);
            }

            _socketRepository.DeleteSocket(model.SocketId);
            _socketRepository.Savechanges();

            return RedirectToAction("Index", "Socket");
        }

        #region MqttHandlers

        [HttpPost]
        public Dictionary<string, bool?> CheckSockets()
        {
            result = new Dictionary<string, bool?>();

            if (_socketRepository.Sockets.Any())
            {
                //TODO send this to view
                string[] topics = _socketRepository.Sockets
                    .Include(s => s.Room)
                    .Where(s => s.Room.UserId.Equals(_userManager.GetUserId(User)))
                    .Select(s => s.DeviceId)
                    .ToArray();

                string[] subTopics = topics.Select(t => "sockets/" + t).ToArray();

                _mqttAppClient.SubscribeToMany(subTopics);
                _mqttAppClient.Client.MqttMsgPublishReceived += AckReceived;

                foreach (string topic in topics)
                {
                    _mqttAppClient.Publish(topic, "Check");
                    result.Add(topic, null);
                }
            }

            System.Threading.Thread.Sleep(1000);

            return result;
        }

        void AckReceived(object sender, MqttMsgPublishEventArgs args)
        {
            result[args.Topic.Split("/")[1]] = Convert.ToBoolean(Encoding.UTF8.GetString(args.Message));
        }

        [HttpPost]
        public void ChangeState(string deviceId, bool value)
        {
            _mqttAppClient.Publish(deviceId, value.ToString());
        }

        #endregion

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