using Smarti.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Smarti.Services
{
    public interface ITimeTaskRepository
    {
        IQueryable<TimeTask> TimeTasks { get; }

        TimeTask GetTimeTaskById(int timeTaskId);
        void CreateTimeTask(TimeTask timeTask);
        void EditTimeTask(TimeTask timeTask);
        void DeleteTimeTask(int timeTaskId);

        void Savechanges();
    }
}
