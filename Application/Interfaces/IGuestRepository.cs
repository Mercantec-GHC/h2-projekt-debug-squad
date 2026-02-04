using Domain;

namespace Application.Interfaces
{
    public interface IGuestRepository
    {
        Task AddAsync(Guest guest);
        Task<IReadOnlyList<Guest>> GetAllAsync();
        Task<Guest> GetByIdAsync(int id);
        Task DeleteByIdAsync(int id);
    }
}
