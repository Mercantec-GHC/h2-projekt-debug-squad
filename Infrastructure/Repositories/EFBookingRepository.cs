using Application.Interfaces;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class EfBookingRepository : IBookingRepository
    {
        private readonly AppDbContext _dbContext;

        public EfBookingRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task AddAsync(Booking booking, int guestId)
        //{
        //    Guest? guest = await _dbContext.Guests.SingleOrDefaultAsync(r => r.Id == guestId);
        //    if (guest == null) return;

        //    guest.AddBooking(booking);

        //    await _dbContext.SaveChangesAsync();
        //}

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _dbContext.Bookings
                .Include(b => b.Room)
                .ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _dbContext.Bookings
                .Include(b => b.Room)
                .SingleOrDefaultAsync(r => r.Id == id);
        }
    }
}
