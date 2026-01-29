using Application.Interfaces;
using Domain;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class EfRoomRepository : IRoomRepository
    {
        private readonly AppDbContext _dbContext;

        public EfRoomRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Room room)
        {
            await _dbContext.Rooms.AddAsync(room);

            await _dbContext.SaveChangesAsync();
        }
    }
}
