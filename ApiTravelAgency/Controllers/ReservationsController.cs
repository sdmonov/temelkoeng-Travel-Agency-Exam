using ApiTravelAgency.DTOS;
using ApiTravelAgency.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiTravelAgency.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationsController : ControllerBase
    {
        
        private IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IEnumerable<ResponseReservationDTO>> GetReservations()
        {
            return await _reservationService.GetReservations();
            }

        [HttpGet("{id}")]
        public async Task<ResponseReservationDTO> GetReservation(long id)
        {
            return await _reservationService.GetReservation(id);
        }

        [HttpPost]
        public async Task<ResponseReservationDTO> CreateReservation(CreateReservationDTO reservation)
        {
            return await _reservationService.CreateReservation(reservation);
        }

        [HttpPut]
        public async Task<ResponseReservationDTO> UpdateReservation(UpdateReservationDTO reservation)
        {
            return await _reservationService.UpdateReservation(reservation);
        }

        [HttpDelete("{id}")]
        public async Task DeleteReservation(long id)
        {
            await _reservationService.DeleteReservation(id);
        }
    }
}
