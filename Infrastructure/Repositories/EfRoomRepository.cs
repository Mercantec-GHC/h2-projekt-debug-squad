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

        public async Task<List<Room>> GetAllAsync()
        {
            return await _dbContext.Rooms.ToListAsync();
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            return await _dbContext.Rooms.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _dbContext.Rooms.Where(r => r.Id == id).ExecuteDeleteAsync();

            await _dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(Room room, int id)
        {
            var existingRoom = await _dbContext.Rooms.FindAsync(id);
            if (existingRoom == null)
                throw new Exception("Room not found");

            existingRoom.Change(room.Number, room.Capacity, room.PricePerNight, room.IsAvailable);

            await _dbContext.SaveChangesAsync();
        }
    }
}
