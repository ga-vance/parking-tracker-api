using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParkingTrackerAPI.Data;
using ParkingTrackerAPI.Dtos.Visit;
using ParkingTrackerAPI.Models;

namespace ParkingTrackerAPI.Services.VisitService
{
    public class VisitService : IVisitService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VisitService(IMapper mapper, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<GetVisitDto>> AddVisit(AddVisitDto newVisit)
        {
            var serviceResponse = new ServiceResponse<GetVisitDto>();
            var userId = Int32.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue("Id")!);
            var visit = _mapper.Map<Visit>(newVisit);
            visit.UserId = userId;
            visit.DateTime = DateTime.UtcNow;
            var dbVehicle = await _context.Vehicles.FirstOrDefaultAsync(
                v => v.PlateNumber == newVisit.PlateNumber.ToUpper()
                && v.PlateRegion == newVisit.PlateRegion.ToUpper());
            if (dbVehicle is null)
            {
                var newVehicle = new Vehicle
                {
                    PlateNumber = newVisit.PlateNumber.ToUpper(),
                    PlateRegion = newVisit.PlateRegion.ToUpper()
                };
                visit.Vehicle = newVehicle;
            }
            else
            {
                visit.VehicleId = dbVehicle.VehicleId;
            }
            try
            {
                _context.Visits.Add(visit);
                await _context.SaveChangesAsync();
                visit = await _context.Visits
                    .Include(v => v.Vehicle)
                    .Include(v => v.Lot)
                    .FirstOrDefaultAsync(v => v.VisitId == visit.VisitId);
                serviceResponse.Data = _mapper.Map<GetVisitDto>(visit);
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<List<GetVisitDto>>> GetVisitsByPlate(string plateNumber, string plateRegion)
        {
            var serviceResponse = new ServiceResponse<List<GetVisitDto>>();

            try
            {
                var visits = await _context.Visits
                    .Include(v => v.Vehicle)
                    .Where(v => v.Vehicle.PlateNumber == plateNumber.ToUpper() && v.Vehicle.PlateRegion == plateRegion.ToUpper())
                    .Include(v => v.Lot)
                    .ToListAsync();
                serviceResponse.Data = visits.Select(v => _mapper.Map<GetVisitDto>(v)).ToList();
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }
        }
    }
}