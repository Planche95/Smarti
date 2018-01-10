using Smarti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Services
{
    public interface ISocketRepository
    {
        IQueryable<Socket> Sockets { get; }

        Socket GetSocketById(int socketId);
        void CreateSocket(Socket socket);
        void EditSocket(Socket socket);
        void DeleteSocket(int socketId);

        void Savechanges(); 
    }
}
