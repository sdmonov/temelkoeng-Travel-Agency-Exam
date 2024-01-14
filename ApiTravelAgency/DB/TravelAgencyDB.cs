namespace ApiTravelAgency.DB
{
    using ApiTravelAgency.Entities;
    using Microsoft.EntityFrameworkCore;

    public class TravelAgencyDB : DbContext
    {
        public TravelAgencyDB(DbContextOptions<TravelAgencyDB> options) : base(options)
        {
        }

        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Holiday)
                .WithMany(h => h.Reservations)
                .HasForeignKey(r => r.HolidayId);

            modelBuilder.Entity<Holiday>()
                .HasOne(h => h.Location)
                .WithMany(l => l.Holidays)
                .HasForeignKey(h => h.LocationId);
        }
    }
}
