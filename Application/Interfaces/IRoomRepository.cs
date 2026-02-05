using Domain;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IRoomRepository
    {
        Task AddAsync(Room room);
        Task<List<Room>> GetAllAsync();
        Task<Room?> GetByIdAsync(int id);
        Task DeleteByIdAsync(int id);
        Task EditAsync(Room room, int id);
        Task<List<Room>> GetFilteredAsync(
            Expression<Func<Room, object>> orderBy,
            int roomAmount,
            bool showOnlyAvailable,
            bool orderDescending);
    }
}
