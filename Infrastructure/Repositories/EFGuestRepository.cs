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
            return await _dbContext.Guests
                .Include(g => g.Bookings)
                .ThenInclude(b => b.Room)
                .ToListAsync();
        }

        public async Task<Guest?> GetByIdAsync(int id)
        {
            Guest? guest = await _dbContext.Guests
                .Include(g => g.Bookings)
                .ThenInclude(b => b.Room)
                .SingleOrDefaultAsync(g => g.Id == id);

            return guest;
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
