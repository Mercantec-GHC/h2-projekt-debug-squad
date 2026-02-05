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

        public async Task AddAsync(Guest guest)
        {
            await _dbContext.Guests.AddAsync(guest);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Guest>> GetAllAsync()
        {
            return await _dbContext.Guests.ToListAsync();
        }

        public async Task<Guest> GetByIdAsync(int id)
        {
            return await _dbContext.Guests.SingleOrDefaultAsync(g => g.Id == id)
                   ?? throw new KeyNotFoundException($"Guest with ID {id} not found.");
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _dbContext.Guests.Where(g => g.Id == id).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();
        }
    }
}

