using AutoMapper;
using ParkingTrackerAPI.Dtos.User;
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
        }
    }
}