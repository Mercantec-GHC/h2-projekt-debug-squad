using Application.Interfaces;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EfGuestRepository : IGuestRepository
    {
        private readonly AppDbContext _dbContext;

        public EfGuestRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Add(Guest guest)
        {
            _dbContext.Guests.Add(guest);
        }

        public async Task<List<Guest>> GetAllAsync()
        {
            return await _dbContext.Guests.Include(g => g.Bookings).ThenInclude(r => r.RoomType).ToListAsync();
        }

        public async Task<Guest?> GetByIdAsync(int id)
        {
            return await _dbContext.Guests
        .Include(g => g.Bookings)
            .ThenInclude(b => b.Room)
        .Include(g => g.Bookings)
            .ThenInclude(b => b.RoomType)
        .SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Guest?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            return await _dbContext.Guests
                .Include(g => g.Bookings)
                    .ThenInclude(b => b.Room)
                .Include(g => g.Bookings)
                    .ThenInclude(b => b.RoomType)
              .SingleOrDefaultAsync(g => g.Email.ToLower() == email.ToLower());
        }

        public void Delete(Guest guest)
        {
            _dbContext.Guests.Remove(guest);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _dbContext.Guests.AnyAsync(g => g.Email == email);
        }

    }
}
