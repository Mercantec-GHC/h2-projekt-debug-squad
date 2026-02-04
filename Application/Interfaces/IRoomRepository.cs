using Domain;

namespace Application.Interfaces
{
    public interface IRoomRepository
    {
        Task AddAsync(Room room);
        Task<List<Room>> GetAllAsync();
        Task<Room> GetByIdAsync(int id);
        Task DeleteByIdAsync(int id);
    }
}
