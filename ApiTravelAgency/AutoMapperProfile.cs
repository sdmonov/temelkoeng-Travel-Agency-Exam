using ApiTravelAgency.DTOS;
using ApiTravelAgency.Entities;
using AutoMapper;

namespace ApiTravelAgency
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateHolidayDTO, Holiday>();
            CreateMap<CreateLocationDTO, Location>();
            CreateMap<Holiday, ResponseHolidayDTO>();
            CreateMap<Location, ResponseLocationDTO>();
            CreateMap<CreateReservationDTO, Reservation>();
            CreateMap<UpdateLocationDTO, Location>();
            CreateMap<UpdateHolidayDTO, Holiday>();
            CreateMap<Reservation, ResponseReservationDTO>();
            CreateMap<UpdateReservationDTO, Reservation>();
        }
    }

}
