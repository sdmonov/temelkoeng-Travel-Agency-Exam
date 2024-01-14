using ApiTravelAgency.DTOS;

namespace ApiTravelAgency.Services
{
    public interface IReservationService
    {
        Task<IEnumerable<ResponseReservationDTO>> GetReservations();
        Task<ResponseReservationDTO> GetReservation(long id);
        Task<ResponseReservationDTO> CreateReservation(CreateReservationDTO reservation);
        Task<ResponseReservationDTO> UpdateReservation(UpdateReservationDTO reservation);
        Task DeleteReservation(long id);
    }
}
