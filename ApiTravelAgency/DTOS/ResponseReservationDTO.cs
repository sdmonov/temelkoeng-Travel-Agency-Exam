namespace ApiTravelAgency.DTOS
{
    public class ResponseReservationDTO
    {
        public long Id { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public ResponseHolidayDTO Holiday { get; set; }
    }
}
