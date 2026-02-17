using Application.Interfaces;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class EfRoomRepository : IRoomRepository
    {
        private readonly AppDbContext _dbContext;

        public EfRoomRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Add(Room room)
        {
            _dbContext.Rooms.Add(room);
        }

        public async Task<List<Room>> GetAllAsync()
        {
            return await _dbContext.Rooms.ToListAsync();
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            return await _dbContext.Rooms.SingleOrDefaultAsync(r => r.Id == id);
        }

        public void Delete(Room room)
        {
            _dbContext.Rooms.Remove(room);
        }

        public async Task<List<Room>> GetFilteredAsync(
            Expression<Func<Room, object>> orderBy,
            int roomAmount = 50,
            bool showOnlyAvailable = true,
            bool orderDescending = true)
        {
            var query = _dbContext.Rooms
                .Where(r => r.IsAvailable == showOnlyAvailable).Take(roomAmount);

            query = orderDescending
                ? query.OrderByDescending(orderBy)
                : query.OrderBy(orderBy);

            return await query.ToListAsync();
        }
    }
}
