using ParkingTrackerAPI.Dtos.Visit;
using ParkingTrackerAPI.Models;

namespace ParkingTrackerAPI.Services.VisitService
{
    public interface IVisitService
    {
        Task<ServiceResponse<GetVisitDto>> AddVisit(AddVisitDto newVisit);

        Task<ServiceResponse<List<GetVisitDto>>> GetVisitsByPlate(string plateNumber, string plateRegion);
    }
}