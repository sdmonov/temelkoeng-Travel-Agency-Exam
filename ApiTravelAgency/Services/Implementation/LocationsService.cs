using ApiTravelAgency.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ApiTravelAgency.DB;
using ApiTravelAgency.Entities;

namespace ApiTravelAgency.Services
{
    public class LocationService : ILocationService
    {
        private readonly TravelAgencyDB _dbContext;
        private readonly IMapper _mapper;

        public LocationService(TravelAgencyDB dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ResponseLocationDTO>> GetLocations()
        {
            var locations = await _dbContext.Locations.ToListAsync();
            return _mapper.Map<IEnumerable<ResponseLocationDTO>>(locations);
        }

        public async Task<ResponseLocationDTO> GetLocation(long id)
        {
            var location = await _dbContext.Locations.FindAsync(id);
            if (location == null)
            {
                throw new KeyNotFoundException("Location not found");
            }

            return _mapper.Map<ResponseLocationDTO>(location);
        }

        public async Task<ResponseLocationDTO> CreateLocation(CreateLocationDTO locationDto)
        {
            var location = _mapper.Map<Location>(locationDto);
            _dbContext.Locations.Add(location);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ResponseLocationDTO>(location);
        }

        public async Task<ResponseLocationDTO> UpdateLocation(UpdateLocationDTO locationDto)
        {
            var location = await _dbContext.Locations.FindAsync(locationDto.Id);
            if (location == null)
            {
                throw new KeyNotFoundException("Location not found");
            }

            _mapper.Map(locationDto, location);
            _dbContext.Locations.Update(location);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ResponseLocationDTO>(location);
        }

        public async Task DeleteLocation(long id)
        {
            var location = await _dbContext.Locations.FindAsync(id);
            if (location != null)
            {
                _dbContext.Locations.Remove(location);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}