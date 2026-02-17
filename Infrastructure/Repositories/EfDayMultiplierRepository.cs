using Application.Interfaces;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class EfDayMultiplierRepository : IDayMultiplierRepository
    {
        private readonly AppDbContext _dbContext;

        public EfDayMultiplierRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<DayMultiplier>> GetAllAsync()
        {
            return await _dbContext.DayMultipliers.ToListAsync();
        }
    }
}
