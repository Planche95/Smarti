using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Smarti.Services;
using AutoMapper;
using Smarti.Models;
using Smarti.Models.TimeTaskViewModel;
using Microsoft.AspNetCore.Authorization;
using Hangfire;
using Hangfire.Storage;

namespace Smarti.Controllers
{
    [Authorize]
    public class TimeTaskController : Controller
    {
        private readonly ITimeTaskRepository _timeTaskRepository;
        private readonly ISocketRepository _socketRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMqttAppClientSingleton _mqttAppClientSingleton;
        private readonly IMapper _mapper;

        public TimeTaskController(ITimeTaskRepository timeTaskRepository, ISocketRepository socketRepository, IAuthorizationService authorizationService, IMqttAppClientSingleton mqttAppClientSingleton, IMapper mapper)
        {
            _timeTaskRepository = timeTaskRepository;
            _socketRepository = socketRepository;
            _authorizationService = authorizationService;
            _mqttAppClientSingleton = mqttAppClientSingleton;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int id)
        {
            Socket socket = _socketRepository.GetSocketById(id);
            TimeTask timeTask = new TimeTask { Socket = socket };

            AuthorizationResult authorizationResult = await _authorizationService
                .AuthorizeAsync(User, timeTask, Operations.Read);

            if (!authorizationResult.Succeeded)
            {
                return new ForbidResult();
            }

            ViewData["SocketName"] = socket.Name;
            ViewData["RoomName"] = socket.Room.Name;

            IEnumerable<TimeTaskListViewModel> timeTaskViewModel = _mapper
                .Map<IEnumerable<TimeTask>, IEnumerable<TimeTaskListViewModel>>(socket.TimeTasks.OrderBy(tt => tt.TimeStamp).ToList());

            return View(timeTaskViewModel);
        }

        public async Task<IActionResult> Create(int id)
        {
            Socket socket = _socketRepository.GetSocketById(id);
            TimeTask timeTask = new TimeTask { Socket = socket, SocketId = id, TimeStamp = DateTime.Today };

            AuthorizationResult authorizationResult = await _authorizationService
                .AuthorizeAsync(User, timeTask, Operations.Create);

            if (!authorizationResult.Succeeded)
            {
                return new ForbidResult();
            }

            TimeTaskCreateViewModel timeTaskViewModel = _mapper.Map<TimeTaskCreateViewModel>(timeTask);

            return View(timeTaskViewModel);
        }

        [HttpPost]
        public IActionResult Create(TimeTaskCreateViewModel model)
        {
            if (model.TimeStamp.CompareTo(DateTime.Now.AddMinutes(1)) <= 0)
            {
                ModelState.AddModelError("", "Date must be minimum 1 minute in the future");
                return View(model);
            }

            TimeTask timeTask = _mapper.Map<TimeTask>(model);

            _timeTaskRepository.CreateTimeTask(timeTask);
            _socketRepository.Savechanges();

            string backgroundJobId = BackgroundJob.Schedule(() => ExecuteTimeTask(timeTask), timeTask.TimeStamp);
            timeTask.BackgroundJobId = backgroundJobId;

            _timeTaskRepository.EditTimeTask(timeTask);
            _socketRepository.Savechanges();

            return RedirectToRoute("TimeTask", new { id = model.SocketId});
        }

        public async Task<IActionResult> Edit(int id)
        {
            TimeTask timeTask = _timeTaskRepository.GetTimeTaskById(id);

            AuthorizationResult authorizationResult = await _authorizationService
                .AuthorizeAsync(User, timeTask, Operations.Update);

            if (!authorizationResult.Succeeded)
            {
                return new ForbidResult();
            }

            TimeTaskEditViewModel timeTaskViewModel = _mapper.Map<TimeTaskEditViewModel>(timeTask);

            return View(timeTaskViewModel);
        }

        [HttpPost]
        public IActionResult Edit(TimeTaskEditViewModel model)
        {
            TimeTask timeTask = _mapper.Map<TimeTask>(model);

            BackgroundJob.Delete(timeTask.BackgroundJobId);
            string backgroundJobId = BackgroundJob.Schedule(() => ExecuteTimeTask(timeTask), timeTask.TimeStamp);
            timeTask.BackgroundJobId = backgroundJobId;

            _timeTaskRepository.EditTimeTask(timeTask);
            _timeTaskRepository.Savechanges();

            return RedirectToAction("Index", "TimeTask", new { id = model.SocketId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            TimeTask timeTask = _timeTaskRepository.GetTimeTaskById(id);

            AuthorizationResult authorizationResult = await _authorizationService
                .AuthorizeAsync(User, timeTask, Operations.Delete);

            if (!authorizationResult.Succeeded)
            {
                return new ForbidResult();
            }

            ViewData["SocketName"] = timeTask.Socket.Name;
            ViewData["RoomName"] = timeTask.Socket.Room.Name;

            TimeTaskDeleteViewModel timeTaskViewModel = _mapper.Map<TimeTaskDeleteViewModel>(timeTask);

            return View(timeTaskViewModel);
        }

        [HttpPost]
        public IActionResult Delete(TimeTaskDeleteViewModel model)
        {
            BackgroundJob.Delete(model.BackgroundJobId);

            _timeTaskRepository.DeleteTimeTask(model.TimeTaskId);
            _timeTaskRepository.Savechanges();

            return RedirectToAction("Index", "TimeTask", new { id = model.SocketId });
        }

        #region Helpers

        public void ExecuteTimeTask(TimeTask timeTask)
        {
            DateTime startTime = DateTime.Now;

            if (startTime.Subtract(timeTask.TimeStamp).Duration().Minutes == 0)
            {
                Socket socket = _socketRepository.GetSocketById(timeTask.SocketId);
                _mqttAppClientSingleton.Publish(socket.DeviceId, timeTask.Action.ToString());
            }

            _timeTaskRepository.DeleteTimeTask(timeTask.TimeTaskId);
            _timeTaskRepository.Savechanges();
        }

        #endregion
    }
}