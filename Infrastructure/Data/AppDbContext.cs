using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    // EF Core DbContext for the application
    // Represents a session with the database and allows querying and saving instances of domain entities.
    public class AppDbContext : DbContext
    {
        // Constructor receives DbContextOptions, which contains configuration (e.g., connection string, provider)
        // This allows configuring the DbContext externally, e.g., in Startup or Program.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // No additional logic needed here for now
        }

        // DbSet represents the collection of Room entities in the database
        // EF Core will use this to query and save Room objects
        public DbSet<Room> Rooms { get; set; } = null!; // null-forgiving because EF Core will initialize it at runtime
    }
}
