using Domain;

namespace Application.Interfaces
{
    public interface IGuestRepository
    {
        Task SaveChangesAsync();
        Task AddAsync(Guest guest);
        Task<List<Guest>> GetAllAsync();
        Task<Guest> GetByIdAsync(int id);
        Task DeleteByIdAsync(int id);
        Task EditAsync(Guest guest, int id);
    }
}
