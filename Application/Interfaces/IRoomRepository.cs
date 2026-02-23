using Domain;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IRoomRepository
    {
        Task SaveChangesAsync();
        void Add(Room room);
        Task<List<Room>> GetAllAsync();
        Task<Room?> GetByIdAsync(int id);
        void Delete(Room room);

        Task<List<Room>> GetAvailableAsync(DateTime requestedCheckIn, DateTime requestedCheckOut, int? capacity, decimal? maxPrice);
    }
}
