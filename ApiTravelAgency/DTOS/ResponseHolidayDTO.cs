namespace ApiTravelAgency.DTOS
{
    public class ResponseHolidayDTO
    {
        public long Id { get; set; }
        public ResponseLocationDTO Location { get; set; }
      
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }
        public int FreeSlots { get; set; }
    }
}
