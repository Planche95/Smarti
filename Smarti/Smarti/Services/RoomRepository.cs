using Smarti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smarti.Models;

namespace Smarti.Services
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RoomRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<Room> Rooms
        {
            get
            {
                return _applicationDbContext.Rooms;
            }
        }

        public void CreateRoom(Room room)
        {
            _applicationDbContext.Add(room);
        }

        public void DeleteRoom(int roomId)
        {
            Room room = _applicationDbContext.Rooms.First(r => r.RoomId == roomId);
            _applicationDbContext.Rooms.Remove(room);
        }

        public void EditRoom(Room room)
        {
            Room editedRoom = _applicationDbContext.Rooms.First(r => r.RoomId == room.RoomId);

            editedRoom.Name = room.Name;
        }

        public Room GetRoomById(int roomId)
        {
            return _applicationDbContext.Rooms.FirstOrDefault(r => r.RoomId == roomId);
        }

        public void Savechanges()
        {
            throw new NotImplementedException();
        }
    }
}
