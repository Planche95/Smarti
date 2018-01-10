using Smarti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Services
{
    public interface IRoomRepository
    {
        IQueryable<Room> Rooms { get; }

        Room GetRoomById(int roomId);
        void CreateRoom(Room room);
        void EditRoom(Room room);
        void DeleteRoom(int roomId);

        void Savechanges();
    }
}
