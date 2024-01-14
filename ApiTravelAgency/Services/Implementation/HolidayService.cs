using ApiTravelAgency.DB;
using ApiTravelAgency.DTOS;
using ApiTravelAgency.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTravelAgency.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly TravelAgencyDB _dbContext;
        private readonly IMapper _mapper;

        public HolidayService(TravelAgencyDB dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ResponseHolidayDTO>> GetHolidays()
        {

            var holidays = await _dbContext.Holidays.ToListAsync();

            //ruchno mapvane
            ResponseHolidayDTO[] holidaysDTO = new ResponseHolidayDTO[holidays.Count];
            for (int i = 0; i < holidays.Count; i++)
            {
                holidaysDTO[i] = new ResponseHolidayDTO();
                holidaysDTO[i].FreeSlots = holidays[i].FreeSlots;

                Location? tempL = _dbContext.Locations.FirstOrDefault(l => l.Id == holidays[i].LocationId);
                holidaysDTO[i].Location = new ResponseLocationDTO()
                {
                    City = tempL.City,
                    Country = tempL.Country,
                    Id = holidays[i].Id,
                    Number = tempL.Number,
                    Street = tempL.Street
                };
                holidaysDTO[i].Duration = holidays[i].Duration;
                holidaysDTO[i].StartDate = holidays[i].StartDate;
                holidaysDTO[i].Title = holidays[i].Title;
                holidaysDTO[i].Price = holidays[i].Price;
                holidaysDTO[i].Id = holidays[i].Id;
            }
            // return _mapper.Map<IEnumerable<ResponseHolidayDTO>>(holidays);
            return holidaysDTO;
        }

        public async Task<ResponseHolidayDTO> GetHoliday(long id)
        {
            var holiday = await _dbContext.Holidays.FindAsync(id);
            ResponseHolidayDTO holidaysDTO = new ResponseHolidayDTO();

            Location? tempL = _dbContext.Locations.FirstOrDefault(l => l.Id == holiday.LocationId);
            holidaysDTO.Location = new ResponseLocationDTO()
            {
                City = tempL.City,
                Country = tempL.Country,
                Id=tempL.Id,
                Number = tempL.Number,
                Street = tempL.Street
            };

            holidaysDTO.FreeSlots = holiday.FreeSlots;
            holidaysDTO.Duration = holiday.Duration;
            holidaysDTO.StartDate = holiday.StartDate;
            holidaysDTO.Title = holiday.Title;
            holidaysDTO.Price = holiday.Price;
            holidaysDTO.Id = holiday.Id;

            if (holiday == null)
            {
                throw new KeyNotFoundException("Holiday not found");
            }
            return holidaysDTO;
        }
        public async Task<IEnumerable<ResponseHolidayDTO>> GetHolidaysByFilter(string location, DateTime startDate, int duration)
        {
            var query = _dbContext.Holidays.Include(h => h.Location).AsQueryable();

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(h => h.Location.City.ToLower() == location || h.Location.Country.ToLower() == location);
            }
         
            if (!startDate.Equals(DateTime.MinValue))
            {
                query = query.Where(h => h.StartDate >= startDate);
            }

            if (duration != 0)
            {
                query = query.Where(h => h.Duration == duration);
            }

            var holidays = await query.ToListAsync();
            return holidays.Select(holiday => new ResponseHolidayDTO
            {
                Id = holiday.Id,
                FreeSlots = holiday.FreeSlots,
                Duration = holiday.Duration,
                StartDate = holiday.StartDate,
                Title = holiday.Title,
                Price = holiday.Price,
                Location = new ResponseLocationDTO
                {
                    Id = holiday.Location.Id,
                    City = holiday.Location.City,
                    Country = holiday.Location.Country,
                    Number = holiday.Location.Number,
                    Street = holiday.Location.Street
                }
            });

        }

        public async Task<ResponseHolidayDTO> CreateHoliday(CreateHolidayDTO holidayDTO)
        {
            var location = await _dbContext.Locations.FindAsync(holidayDTO.Location);
            if (location == null)
            {
                throw new KeyNotFoundException($"Location with Id {holidayDTO.Location} not found");
            }

            Holiday holiday = new Holiday()
            {
                Duration = holidayDTO.Duration,
                FreeSlots = holidayDTO.FreeSlots,
                LocationId = holidayDTO.Location,
                Price = holidayDTO.Price,
                StartDate = (DateTime)holidayDTO.StartDate,
                Title = holidayDTO.Title
            };

            _dbContext.Holidays.Add(holiday);
            await _dbContext.SaveChangesAsync();

          
            var responseHolidayDTO = new ResponseHolidayDTO
            {
                FreeSlots = holiday.FreeSlots,
                Duration = holiday.Duration,
                StartDate = holiday.StartDate,
                Title = holiday.Title,
                Price = holiday.Price,
                Id = holiday.Id
            };

            return responseHolidayDTO;
        }

        public async Task<ResponseHolidayDTO> UpdateHoliday(UpdateHolidayDTO holidayDTO)
        {
            var holiday = await _dbContext.Holidays.FindAsync(holidayDTO.Id);
            if (holiday == null)
            {
                throw new KeyNotFoundException("Holiday not found");
            }

            holiday.LocationId=holidayDTO.Location;
            holiday.Title = holidayDTO.Title;
            holiday.StartDate = holidayDTO.StartDate;
            holiday.Duration = holidayDTO.Duration;
            holiday.Price = holidayDTO.Price;
            holiday.FreeSlots = holidayDTO.FreeSlots;
          

            _dbContext.Holidays.Update(holiday);
            await _dbContext.SaveChangesAsync();

         
            var responseHolidayDTO = new ResponseHolidayDTO
            {
                Id = holiday.Id,
                FreeSlots = holiday.FreeSlots,
                Duration = holiday.Duration,
                StartDate = holiday.StartDate,
                Title = holiday.Title,
                Price = holiday.Price
            };

            return responseHolidayDTO;
        }

        public async Task DeleteHoliday(long id)
        {
            var holiday = await _dbContext.Holidays.FindAsync(id);
            _dbContext.Holidays.Remove(holiday);
            await _dbContext.SaveChangesAsync();
        }

      
    }
}