using ParkingTrackerAPI.Dtos.Visit;
using ParkingTrackerAPI.Models;

namespace ParkingTrackerAPI.Services.VisitService
{
    public class VisitService : IVisitService
    {
        public Task<ServiceResponse<GetVisitDto>> AddVisit(AddVisitDto newVisit)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<GetVisitDto>>> GetVisitsByPlate(string plateNumber, string plateRegion)
        {
            throw new NotImplementedException();
        }
    }
}