using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChartJSCore.Models;
using Smarti.Services;
using Smarti.Models;
using Microsoft.EntityFrameworkCore;
using Smarti.Models.StatisticsViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Smarti.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IChartGenerator _chartGenerator;

        public StatisticsController(IRoomRepository roomRepository, IChartGenerator chartGenerator)
        {
            _roomRepository = roomRepository;
            _chartGenerator = chartGenerator;
        }

        public IActionResult Index(DateTime From, DateTime To)
        {
            //To get data for one day from (From == To)
            To = To.AddDays(1);

            if (From.Equals(new DateTime(1, 1, 1)) || To.Equals(new DateTime(1, 1, 2)))
            {
                From = DateTime.Today.AddDays(-1);
                To = DateTime.Today;

                ModelState.AddModelError("", "Dates can't be empty! Statistics are displayed for " + DateTime.Today.AddDays(-1).ToShortDateString());
            }

            if (DateTime.Compare(From, To) > 0)
            {
                From = DateTime.Today.AddDays(-1);
                To = DateTime.Today;

                ModelState.AddModelError("", "Date \"To\" can't be before date \"From\"! Statistics are displayed for " + DateTime.Today.AddDays(-1).ToShortDateString());
            }

            List<Room> rooms = _roomRepository.Rooms
                                            .Include(r => r.Sockets)
                                            .ThenInclude(s => s.SocketDatas)
                                            .ToList();

            List<StatisticsViewModel> viewModel = new List<StatisticsViewModel>
            {
                new StatisticsViewModel
                {
                    Header = "All rooms",
                    PieChart = GeneratePieChart(rooms, From, To),
                    BarChart = GenerateBarChart(rooms, From, To),
                    Expanded = true
                }
            };

            for (int i = 0; i < rooms.Count(); i++)
            {
                viewModel.Add(
                    new StatisticsViewModel
                    {
                        Header = rooms[i].Name,
                        PieChart = GeneratePieChart(rooms[i], From, To),
                        BarChart = GenerateBarChart(rooms[i], From, To),
                        Expanded = false
                    }
                );
            }

            return View(viewModel);
        }

        #region Helpers

        private Chart GeneratePieChart(Room room, DateTime from, DateTime to)
        {
            List<string> socketsNames = room.Sockets.Select(s => s.Name).ToList();

            List<string> backgroundColors = new List<string>();
            var random = new Random();

            for (int j = 0; j < socketsNames.Count(); j++)
            {
                backgroundColors.Add(String.Format("#{0:X6}", random.Next(0x1000000)));
            }

            List<double> data = new List<double>();
            for (int j = 0; j < socketsNames.Count(); j++)
            {
                double sum = room.Sockets
                        .Where(s => s.Name.Equals(socketsNames[j]))
                        .SelectMany(s => s.SocketDatas)
                        .Where(sd => sd.TimeStamp.CompareTo(from) >= 0 && sd.TimeStamp.CompareTo(to) < 0)
                        .Sum(sd => sd.Value);

                data.Add(Math.Round(sum, 2));
            }

            PieDataset dataset = new PieDataset()
            {
                Label = "PieDataser",
                BackgroundColor = backgroundColors,
                HoverBackgroundColor = backgroundColors,
                Data = data
            };

            return _chartGenerator.GeneratePieChart(socketsNames, dataset);
        }

        private Chart GenerateBarChart(Room room, DateTime from, DateTime to)
        {
            List<SocketData> validatedData = room.Sockets
                        .SelectMany(s => s.SocketDatas)
                        .Where(sd => sd.TimeStamp.CompareTo(from) >= 0 && sd.TimeStamp.CompareTo(to) < 0)
                        .ToList();

            List<double> data = new List<double>();
            for (int i = 0; i < 7; i++)
            {
                double sum = validatedData.Where(sd => (int)sd.TimeStamp.DayOfWeek == i).Sum(sd => sd.Value);

                data.Add(Math.Round(sum, 2));
            }

            //Default index 0 is for Sunday
            data.Add(data[0]);
            data.RemoveAt(0);

            return _chartGenerator.GenerateBarChart(data);
        }

        private Chart GeneratePieChart(List<Room> rooms, DateTime from, DateTime to)
        {
            List<string> roomsNames = rooms.Select(r => r.Name).ToList();

            List<string> backgroundColors = new List<string>();
            var random = new Random();

            for (int i = 0; i < roomsNames.Count(); i++)
            {
                backgroundColors.Add(String.Format("#{0:X6}", random.Next(0x1000000)));
            }

            List<double> data = new List<double>();
            for (int i = 0; i < rooms.Count(); i++)
            {
                double sum = rooms.Where(r => r.Name.Equals(rooms[i].Name))
                                    .SelectMany(r => r.Sockets)
                                    .SelectMany(s => s.SocketDatas)
                                    .Where(sd => sd.TimeStamp.CompareTo(from) >= 0 && sd.TimeStamp.CompareTo(to) < 0)
                                    .Sum(sd => sd.Value);

                data.Add(Math.Round(sum, 2));
            }

            PieDataset dataset = new PieDataset()
            {
                Label = "PieDataser",
                BackgroundColor = backgroundColors,
                HoverBackgroundColor = backgroundColors,
                Data = data
            };

            return _chartGenerator.GeneratePieChart(roomsNames, dataset);
        }

        private Chart GenerateBarChart(List<Room> rooms, DateTime from, DateTime to)
        {
            List<SocketData> validatedData = rooms
                        .SelectMany(r => r.Sockets)
                        .SelectMany(s => s.SocketDatas)
                        .Where(sd => sd.TimeStamp.CompareTo(from) >= 0 && sd.TimeStamp.CompareTo(to) < 0)
                        .ToList();

            List<double> data = new List<double>();
            for (int i = 0; i < 7; i++)
            {
                double sum = validatedData.Where(sd => (int)sd.TimeStamp.DayOfWeek == i).Sum(sd => sd.Value);

                data.Add(Math.Round(sum, 2));
            }

            //Default index 0 is for Sunday
            data.Add(data[0]);
            data.RemoveAt(0);

            return _chartGenerator.GenerateBarChart(data);
        }

        #endregion
    }
}