using ParkingTrackerAPI.Dtos.Lot;
using ParkingTrackerAPI.Models;

namespace ParkingTrackerAPI.Services.LotService
{
    public interface ILotService
    {
        Task<ServiceResponse<GetLotDto>> AddLot(AddLotDto newLot);

        Task<ServiceResponse<List<GetLotDto>>> GetAllLots();
    }
}