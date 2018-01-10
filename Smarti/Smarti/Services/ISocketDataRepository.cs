using Smarti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Services
{
    public interface ISocketDataRepository
    {
        IQueryable<SocketData> GetSocketDatasForUser(string userName);
    }
}
