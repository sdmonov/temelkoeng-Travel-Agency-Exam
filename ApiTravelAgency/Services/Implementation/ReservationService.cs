using ApiTravelAgency.DB;
using ApiTravelAgency.DTOS;
using ApiTravelAgency.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTravelAgency.Services
{
    public class ReservationService : IReservationService
    {
        private readonly TravelAgencyDB _dbContext;
        private readonly IMapper _mapper;
        private readonly IHolidayService _holidayService;

        public ReservationService(TravelAgencyDB dbContext, IMapper mapper, IHolidayService holidayService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _holidayService = holidayService;
        }

        public async Task<IEnumerable<ResponseReservationDTO>> GetReservations()
        {
            var reservations = await _dbContext.Reservations.ToListAsync();
            List<ResponseReservationDTO> responses = _mapper.Map<List<ResponseReservationDTO>>(reservations);
            for (int i = 0; i < responses.Count; i++)
            {
                responses[i].Holiday = await _holidayService.GetHoliday(reservations[i].HolidayId);
            }
            
           
            return responses;
        }

        public async Task<ResponseReservationDTO> GetReservation(long id)
        {
            var reservation = await _dbContext.Reservations.FindAsync(id);
            ResponseReservationDTO response=_mapper.Map<ResponseReservationDTO>(reservation);

            response.Holiday = await _holidayService.GetHoliday(reservation.HolidayId);
            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found");
            }
            return response;
        }

        public async Task<ResponseReservationDTO> CreateReservation(CreateReservationDTO reservationDTO)
        {

            Reservation reservation = new Reservation()
            {
                ContactName = reservationDTO.ContactName,
                PhoneNumber = reservationDTO.PhoneNumber,
                HolidayId = reservationDTO.Holiday
            };
            _dbContext.Reservations.Add(reservation);
        
            await _dbContext.SaveChangesAsync();
           
            return _mapper.Map<ResponseReservationDTO>(reservation);
        }

        public async Task<ResponseReservationDTO> UpdateReservation(UpdateReservationDTO reservationDTO)
        {
            Reservation reservation = await _dbContext.Reservations.FindAsync(reservationDTO.Id);
            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found");
            }
            reservation.HolidayId = reservationDTO.Holiday;
            reservation.ContactName = reservationDTO.ContactName;
            reservation.PhoneNumber = reservationDTO.PhoneNumber;
            _dbContext.Reservations.Update(reservation);
            await _dbContext.SaveChangesAsync();

            //manuel map
            ResponseReservationDTO responseReservation = new ResponseReservationDTO()
            {
                ContactName = reservation.ContactName,
                Holiday = await _holidayService.GetHoliday(reservationDTO.Holiday),
                Id = reservation.Id,
                PhoneNumber = reservation.PhoneNumber
            };
            return responseReservation;
        }

        public async Task DeleteReservation(long id)
        {
            var reservation = await _dbContext.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _dbContext.Reservations.Remove(reservation);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}