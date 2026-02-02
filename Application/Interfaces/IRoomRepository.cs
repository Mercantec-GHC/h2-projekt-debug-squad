using Domain;

namespace Application.Interfaces
{
    public interface IRoomRepository
    {
        Task AddAsync(Room room);

        Task<IReadOnlyList<Room>> GetAllAsync();
    }
}
