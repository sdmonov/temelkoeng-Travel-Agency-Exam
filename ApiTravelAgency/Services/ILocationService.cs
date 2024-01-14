using ApiTravelAgency.DTOS;

namespace ApiTravelAgency.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<ResponseLocationDTO>> GetLocations();
        Task<ResponseLocationDTO> GetLocation(long id);
        Task<ResponseLocationDTO> CreateLocation(CreateLocationDTO location);
        Task<ResponseLocationDTO> UpdateLocation(UpdateLocationDTO location);
        Task DeleteLocation(long id);
    }
}
