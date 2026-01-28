using Application.Interfaces;
using Domain;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    // Concrete implementation of IRoomRepository using EF Core
    // This class is part of the Infrastructure layer and knows how to persist Room entities to the database
    public class EfRoomRepository : IRoomRepository
    {
        // EF Core DbContext, injected via constructor
        // Provides access to the database and tracks entity changes
        private readonly AppDbContext _dbContext;

        // Constructor receives the AppDbContext via Dependency Injection
        // This allows the repository to interact with the database without creating DbContext itself
        public EfRoomRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Adds a new Room entity to the database
        // This method implements the IRoomRepository abstraction
        public async Task AddAsync(Room room)
        {
            // Add the Room entity to the DbSet
            // EF Core marks it as Added in its change tracker
            await _dbContext.Rooms.AddAsync(room);

            // Commit the changes to the database
            await _dbContext.SaveChangesAsync();
        }
    }
}
