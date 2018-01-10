using Smarti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smarti.Models;

namespace Smarti.Services
{
    public class SocketDataRepository : ISocketDataRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SocketDataRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<SocketData> GetSocketDatasForUser(string userName)
        {
            return _applicationDbContext.SocketDatas
                .Where(sd => sd.Socket.Room.ApplicationUser.UserName == userName);
            throw new NotImplementedException();
        }
    }
}
