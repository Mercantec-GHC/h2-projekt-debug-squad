using Application.Interfaces;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EfRoomTypeRepository : IRoomTypeRepository
    {
        private readonly AppDbContext _dbContext;

        public EfRoomTypeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Add(RoomType roomType)
        {
            _dbContext.RoomTypes.Add(roomType);
        }

        public async Task<List<RoomType>> GetAllAsync()
        {
            return await _dbContext.RoomTypes.ToListAsync();
        }

        public async Task<RoomType?> GetByIdAsync(int id)
        {
            return await _dbContext.RoomTypes.SingleOrDefaultAsync(rt => rt.Id == id);
        }

        public void Delete(RoomType roomType)
        {
            _dbContext.RoomTypes.Remove(roomType);
        }
    }
}
