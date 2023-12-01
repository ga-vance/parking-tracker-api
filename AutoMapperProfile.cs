using AutoMapper;
using ParkingTrackerAPI.Dtos.Lot;
using ParkingTrackerAPI.Dtos.User;
using ParkingTrackerAPI.Dtos.Visit;
using ParkingTrackerAPI.Models;

namespace ParkingTrackerAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddUserDto, User>();
            CreateMap<User, GetUserDto>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<AddLotDto, Lot>();
            CreateMap<Lot, GetLotDto>();
            CreateMap<Visit, GetVisitDto>();
            CreateMap<AddVisitDto, Visit>();
        }
    }
}