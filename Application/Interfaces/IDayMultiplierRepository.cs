using Domain;

namespace Application.Interfaces
{
    public interface IDayMultiplierRepository
    {
        Task SaveChangesAsync();
        Task<List<DayMultiplier>> GetAllAsync();
        Task<DayMultiplier?> GetByIdAsync(int id);
    }
}
