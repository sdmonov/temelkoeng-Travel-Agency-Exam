using ApiTravelAgency.DTOS;
using ApiTravelAgency.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiTravelAgency.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        
        private ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IEnumerable<ResponseLocationDTO>> GetLocations()
        {
            return await _locationService.GetLocations();
        }

        [HttpGet("{id}")]
        public async Task<ResponseLocationDTO> GetLocation(long id)
        {
            return await _locationService.GetLocation(id);
        }

        [HttpPost]
        public async Task<ResponseLocationDTO> CreateLocation(CreateLocationDTO location)
        {
            return await _locationService.CreateLocation(location);
        }

        [HttpPut]
        public async Task<ResponseLocationDTO> UpdateLocation(UpdateLocationDTO location)
        {
            return await _locationService.UpdateLocation(location);
        }

        [HttpDelete("{id}")]
        public async Task DeleteLocation(long id)
        {
            await _locationService.DeleteLocation(id);
        }
    }
}
