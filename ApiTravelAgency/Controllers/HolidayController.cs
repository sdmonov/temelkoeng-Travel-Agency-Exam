using ApiTravelAgency.DTOS;
using ApiTravelAgency.Entities;
using ApiTravelAgency.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiTravelAgency.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HolidaysController : ControllerBase
    {
        
        private IHolidayService _holidayService;

        public HolidaysController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        [HttpGet]
        public async Task<IEnumerable<ResponseHolidayDTO>> GetHolidays(string? location, DateTime startDate, int duration)
        {
            if (location != null || !startDate.Equals(DateTime.MinValue) || duration != 0)
            {

                return await _holidayService.GetHolidaysByFilter(location, startDate, duration);
            }

            return await _holidayService.GetHolidays();

        }


        [HttpGet("{id}")]
        public async Task<ResponseHolidayDTO> GetHoliday(long id)
        {
            return await _holidayService.GetHoliday(id);
        }

        [HttpPost]
        public async Task<ResponseHolidayDTO> CreateHoliday(CreateHolidayDTO holiday)
        {
            return await _holidayService.CreateHoliday(holiday);
        }

        [HttpPut]
        public async Task<ResponseHolidayDTO> UpdateHoliday(UpdateHolidayDTO holiday)
        {
            return await _holidayService.UpdateHoliday(holiday);
        }

        [HttpDelete("{id}")]
        public async Task DeleteHoliday(long id)
        {
            await _holidayService.DeleteHoliday(id);
        }
    }

}
