using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Guest> Guests { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Calls the base implementation of OnModelCreating, which is a good practice to ensure any default behavior is preserved.

            modelBuilder.Entity<Booking>()
                .HasOne(booking => booking.Guest)        // Each Booking has one Guest
                .WithMany(guest => guest.Bookings)       // Each Guest can have many Bookings
                .HasForeignKey("GuestId")                // The foreign key column in Booking table is GuestId
                .OnDelete(DeleteBehavior.Cascade);       // If a Guest is deleted, all their Bookings are automatically deleted

            modelBuilder.Entity<Booking>()
                .HasOne(booking => booking.Room)         // Each Booking has one Room
                .WithMany()                              // We are not keeping a collection of Bookings inside Room
                .HasForeignKey("RoomId")                 // The foreign key column in Booking table is RoomId
                .OnDelete(DeleteBehavior.Restrict);      // Cannot delete a Room if there are any Bookings referencing it
        }
    }
}