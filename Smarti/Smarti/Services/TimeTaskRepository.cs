using Smarti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smarti.Models;
using Microsoft.EntityFrameworkCore;

namespace Smarti.Services
{
    public class TimeTaskRepository : ITimeTaskRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TimeTaskRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<TimeTask> TimeTasks
        {
            get
            {
                return _applicationDbContext.TimeTasks;
            }
        }

        public void CreateTimeTask(TimeTask timeTask)
        {
            _applicationDbContext.TimeTasks.Add(timeTask);
        }

        public void DeleteTimeTask(int timeTaskId)
        {
            TimeTask timeTask = _applicationDbContext.TimeTasks.First(tt => tt.TimeTaskId == timeTaskId);
            _applicationDbContext.TimeTasks.Remove(timeTask);
        }

        public void EditTimeTask(TimeTask timeTask)
        {
            TimeTask editedTimeTask = _applicationDbContext.TimeTasks.First(tt => tt.TimeTaskId == timeTask.TimeTaskId);

            editedTimeTask.Action = timeTask.Action;
            editedTimeTask.TimeStamp = timeTask.TimeStamp;
        }

        public TimeTask GetTimeTaskById(int timeTaskId)
        {
            return _applicationDbContext.TimeTasks
                .Where(tt => tt.TimeTaskId == timeTaskId)
                .Include(tt => tt.Socket)
                .ThenInclude(s => s.Room)
                .FirstOrDefault();
        }

        public void Savechanges()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
