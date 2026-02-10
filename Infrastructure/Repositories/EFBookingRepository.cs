using Application.Interfaces;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class EFBookingRepository : IBookingRepository
    {
        private readonly AppDbContext _dbContext;

        public EFBookingRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Booking booking, int guestId)
        {
            Guest? guest = await _dbContext.Guests.SingleOrDefaultAsync(r => r.Id == guestId);
            if (guest == null) return;

            guest.AddBooking(booking);

            await _dbContext.SaveChangesAsync();
        }
    }
}
