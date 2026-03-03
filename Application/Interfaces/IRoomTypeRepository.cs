using Domain;

namespace Application.Interfaces
{
    public interface IRoomTypeRepository
    {
        Task SaveChangesAsync();
        void Add(RoomType roomType);
        Task<List<RoomType>> GetAllAsync(bool includeRooms = false);
        Task<RoomType?> GetByIdAsync(int id);
        void Delete(RoomType roomType);
        Task<List<RoomType>> GetAvailableAsync(
            int? capacity,
            decimal? maxPrice,
            DateTime requestedCheckIn,
            DateTime requestedCheckOut);
        Task<int> GetOverlappingAsync(int roomTypeId, DateTime checkInDate, DateTime checkOutDate);
        Task<int> RoomCountAsync(int roomTypeId);
    }
}
