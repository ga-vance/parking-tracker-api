using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingTrackerAPI.Dtos.Visit;
using ParkingTrackerAPI.Models;
using ParkingTrackerAPI.Services.VisitService;

namespace ParkingTrackerAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class VisitController : ControllerBase
    {
        private readonly IVisitService _visitService;

        public VisitController(IVisitService visitService)
        {
            _visitService = visitService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetVisitDto>>>> GetVisitsByPlate(string plateNumber, string plateRegion)
        {
            ServiceResponse<List<GetVisitDto>> response = await _visitService.GetVisitsByPlate(plateNumber, plateRegion);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetVisitDto>>> AddVisit(AddVisitDto newVisit)
        {
            ServiceResponse<GetVisitDto> response = await _visitService.AddVisit(newVisit);
            return Ok(response);
        }
    }
}