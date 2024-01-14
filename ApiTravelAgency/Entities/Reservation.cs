namespace ApiTravelAgency.Entities
{
    public class Reservation
    {
        public long Id { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public long HolidayId { get; set; }
        public Holiday Holiday { get; set; }
    }
}
