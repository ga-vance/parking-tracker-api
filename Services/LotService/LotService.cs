using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParkingTrackerAPI.Data;
using ParkingTrackerAPI.Dtos.Lot;
using ParkingTrackerAPI.Models;

namespace ParkingTrackerAPI.Services.LotService
{
    public class LotService : ILotService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LotService(IMapper mapper, ApplicationDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetLotDto>> AddLot(AddLotDto newLot)
        {
            var serviceResponse = new ServiceResponse<GetLotDto>();
            var lot = _mapper.Map<Lot>(newLot);
            var checkLot = await _context.Lots.FirstOrDefaultAsync(l => l.LotCode == newLot.LotCode);
            if (checkLot is not null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Must have unique lot code";
                return serviceResponse;
            }
            try
            {
                _context.Lots.Add(lot);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetLotDto>(lot);
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetLotDto>>> GetAllLots()
        {
            var serviceResponse = new ServiceResponse<List<GetLotDto>>();
            var lots = await _context.Lots.ToListAsync();
            serviceResponse.Data = lots.Select(l => _mapper.Map<GetLotDto>(l)).ToList();
            return serviceResponse;
        }
    }
}