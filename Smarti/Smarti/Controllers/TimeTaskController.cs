using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Smarti.Services;
using AutoMapper;
using Smarti.Models;
using Smarti.Models.TimeTaskViewModel;

namespace Smarti.Controllers
{
    public class TimeTaskController : Controller
    {
        private readonly ITimeTaskRepository _timeTaskRepository;
        private readonly ISocketRepository _socketRepository;
        private readonly IMapper _mapper;

        public TimeTaskController(ITimeTaskRepository timeTaskRepository, ISocketRepository socketRepository, IMapper mapper)
        {
            _timeTaskRepository = timeTaskRepository;
            _socketRepository = socketRepository;
            _mapper = mapper;
        }

        public IActionResult Index(int id)
        {
            Socket socket = _socketRepository.GetSocketById(id);
            ViewData["SocketName"] = socket.Name;
            ViewData["RoomName"] = socket.Room.Name;

            IEnumerable<TimeTaskListViewModel> timeTaskViewModel = _mapper
                .Map<IEnumerable<TimeTask>, IEnumerable<TimeTaskListViewModel>>(socket.TimeTasks.ToList());

            return View(timeTaskViewModel);
        }

        public IActionResult Create(int id)
        {
            return View(new TimeTaskCreateViewModel { SocketId = id });
        }

        [HttpPost]
        public IActionResult Create(TimeTaskCreateViewModel model)
        {
            TimeTask timeTask = _mapper.Map<TimeTask>(model);
            _timeTaskRepository.CreateTimeTask(timeTask);
            _socketRepository.Savechanges();

            return RedirectToAction("Index", "TimeTask", new { id = model.SocketId});
        }

        public IActionResult Edit(int id)
        {
            TimeTask timeTask = _timeTaskRepository.GetTimeTaskById(id);
            TimeTaskEditViewModel timeTaskViewModel = _mapper.Map<TimeTaskEditViewModel>(timeTask);

            return View(timeTaskViewModel);
        }

        [HttpPost]
        public IActionResult Edit(TimeTaskEditViewModel model)
        {
            TimeTask timeTask = _mapper.Map<TimeTask>(model);
            _timeTaskRepository.EditTimeTask(timeTask);
            _timeTaskRepository.Savechanges();

            return RedirectToAction("Index", "TimeTask", new { id = model.SocketId });
        }

        public IActionResult Delete(int id)
        {
            TimeTask timeTask = _timeTaskRepository.GetTimeTaskById(id);
            
            ViewData["SocketName"] = timeTask.Socket.Name;
            ViewData["RoomName"] = timeTask.Socket.Room.Name;

            TimeTaskDeleteViewModel timeTaskViewModel = _mapper.Map<TimeTaskDeleteViewModel>(timeTask);

            return View(timeTaskViewModel);
        }

        [HttpPost]
        public IActionResult Delete(TimeTaskDeleteViewModel model)
        {
            _timeTaskRepository.DeleteTimeTask(model.TimeTaskId);
            _timeTaskRepository.Savechanges();

            return RedirectToAction("Index", "TimeTask", new { id = model.SocketId });
        }
    }
}