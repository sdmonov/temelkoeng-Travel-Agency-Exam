using ApiTravelAgency.DTOS;

namespace ApiTravelAgency.Services
{
    public interface IHolidayService
    {
        Task<IEnumerable<ResponseHolidayDTO>> GetHolidays();
        Task<IEnumerable<ResponseHolidayDTO>> GetHolidaysByFilter(string location, DateTime startDate, int duration);
        Task<ResponseHolidayDTO> GetHoliday(long id);
        Task<ResponseHolidayDTO> CreateHoliday(CreateHolidayDTO holiday);
        Task<ResponseHolidayDTO> UpdateHoliday(UpdateHolidayDTO holiday);
        Task DeleteHoliday(long id);
       
    }
}
