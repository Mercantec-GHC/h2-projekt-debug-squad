using Domain;

namespace Application.Interfaces
{
    public interface IGuestRepository
    {
        Task SaveChangesAsync();
        void Add(Guest guest);
        Task<List<Guest>> GetAllAsync();
        Task<Guest?> GetByIdAsync(int id);
        void Delete(Guest guest);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
