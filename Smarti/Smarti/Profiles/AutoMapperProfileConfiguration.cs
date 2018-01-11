using AutoMapper;
using Smarti.Models;
using Smarti.Models.RoomViewModels;
using Smarti.Models.SocketsViewModels;
using Smarti.Models.TimeTaskViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smarti.Profiles
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<Socket, SocketCreateViewModel>().ReverseMap();
            CreateMap<Socket, SocketEditViewModel>().ReverseMap();
            CreateMap<Socket, SocketDeleteViewModel>();
            CreateMap<Socket, SocketListViewModel>();

            CreateMap<Room, RoomCreateViewModel>().ReverseMap();
            CreateMap<Room, RoomEditViewModel>().ReverseMap();
            CreateMap<Room, RoomDeleteViewModel>();
            CreateMap<Room, RoomListViewModel>()
                .ForMember(dest => dest.Sockets, source => source.MapFrom(nested => nested.Sockets));

            CreateMap<TimeTask, TimeTaskCreateViewModel>().ReverseMap();
            CreateMap<TimeTask, TimeTaskEditViewModel>().ReverseMap();
            CreateMap<TimeTask, TimeTaskDeleteViewModel>();
            CreateMap<TimeTask, TimeTaskListViewModel>();
        }
    }
}
