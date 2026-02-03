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
            return await _dbContext.Rooms.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _dbContext.Rooms
                .Where(r => r.Id == id)
                .ExecuteDeleteAsync();

            await _dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(Room room)
        {
            var existingRoom = await _dbContext.Rooms.FindAsync(room.Id);
            if (existingRoom == null)
                throw new Exception("Room not found");

            existingRoom.Name = room.Name;
            existingRoom.Capacity = room.Capacity;
            // update only what you allow

            await _dbContext.SaveChangesAsync();
        }
    }
}
