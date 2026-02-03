using Application.Interfaces;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IReadOnlyList<Room>> GetAllAsync()
        {
            return await _dbContext.Rooms.ToListAsync();
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _dbContext.Rooms.SingleOrDefaultAsync(f => f.Id == id);
        }
    }
}
