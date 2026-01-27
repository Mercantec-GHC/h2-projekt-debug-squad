using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet<Entity> пока пусто
        // public DbSet<Room> Rooms => Set<Room>();
    }
}
