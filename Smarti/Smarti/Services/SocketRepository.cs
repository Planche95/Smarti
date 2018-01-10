using Smarti.Data;
using Smarti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Services
{
    public class SocketRepository : ISocketRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SocketRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<Socket> Sockets
        {
            get
            {
                return _applicationDbContext.Sockets;
            }
        }

        public void CreateSocket(Socket socket)
        {
            _applicationDbContext.Sockets.Add(socket);
        }

        public void DeleteSocket(int socketId)
        {
            Socket socket = _applicationDbContext.Sockets.First(s => s.SocketId == socketId);
            _applicationDbContext.Sockets.Remove(socket);
        }

        public void EditSocket(Socket socket)
        {
            Socket editedSocket = _applicationDbContext.Sockets.First(s => s.SocketId == socket.SocketId);

            editedSocket.DeviceId = socket.DeviceId;
            editedSocket.Name = socket.Name;
            editedSocket.RoomId = socket.RoomId;
        }

        public Socket GetSocketById(int socketId)
        {
            return _applicationDbContext.Sockets.FirstOrDefault(s => s.SocketId == socketId);
        }

        public void Savechanges()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
