using Domain;

namespace Application.Interfaces
{
    public interface IRoomRepository
    {
        Task AddAsync(Room room);
        Task<IReadOnlyList<Room>> GetAllAsync();
        Task<Room> GetByIdAsync(int id);
        Task DeleteByIdAsync(int id);
    }
}
