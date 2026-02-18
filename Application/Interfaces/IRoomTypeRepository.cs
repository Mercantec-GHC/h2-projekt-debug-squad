using Domain;

namespace Application.Interfaces
{
    public interface IRoomTypeRepository
    {
        Task SaveChangesAsync();
        void Add(RoomType roomType);
        Task<List<RoomType>> GetAllAsync();
        Task<RoomType?> GetByIdAsync(int id);
        void Delete(RoomType roomType);
    }
}
