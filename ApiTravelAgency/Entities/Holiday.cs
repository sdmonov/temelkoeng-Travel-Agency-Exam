namespace ApiTravelAgency.Entities
{
    public class Holiday
    {
        public long Id { get; set; }
        public Location Location { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }
        public int FreeSlots { get; set; }
        public long LocationId { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
