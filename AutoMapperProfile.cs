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
            CreateMap<Visit, GetVisitDto>()
                .ForMember(dest => dest.PlateNumber, opt => opt.MapFrom(src => src.Vehicle.PlateNumber))
                .ForMember(dest => dest.PlateRegion, opt => opt.MapFrom(src => src.Vehicle.PlateRegion))
                .ForMember(dest => dest.LotCode, opt => opt.MapFrom(src => src.Lot.LotCode))
                .ForMember(dest => dest.LotName, opt => opt.MapFrom(src => src.Lot.LotName));
            CreateMap<AddVisitDto, Visit>();
        }
    }
}