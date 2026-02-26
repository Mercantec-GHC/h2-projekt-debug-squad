using Application.Interfaces;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EfBookingRepository : IBookingRepository
    {
        private readonly AppDbContext _dbContext;

        public EfBookingRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _dbContext.Bookings.Include(b => b.Room).Include(r => r.RoomType). Include(b => b.Guest)
           .ToListAsync();

        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _dbContext.Bookings.Include(b => b.Room).Include(r => r.RoomType).SingleOrDefaultAsync(r => r.Id == id);
        }
    }
}
