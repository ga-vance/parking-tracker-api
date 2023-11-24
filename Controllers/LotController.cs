using Microsoft.AspNetCore.Mvc;
using ParkingTrackerAPI.Dtos.Lot;
using ParkingTrackerAPI.Models;
using ParkingTrackerAPI.Services.LotService;

namespace ParkingTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotController : ControllerBase
    {
        private readonly ILotService _lotService;

        public LotController(ILotService lotService)
        {
            _lotService = lotService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetLotDto>>> AddLot(AddLotDto newLot)
        {
            ServiceResponse<GetLotDto> response = await _lotService.AddLot(newLot);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetLotDto>>>> GetAllLots()
        {
            ServiceResponse<List<GetLotDto>> response = await _lotService.GetAllLots();
            return Ok(response);
        }
    }
}