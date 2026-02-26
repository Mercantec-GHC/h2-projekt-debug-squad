using Application.Interfaces;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infrastructure.Repositories
{
    public class EfRoomTypeRepository : IRoomTypeRepository
    {
        private readonly AppDbContext _dbContext;

        public EfRoomTypeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Add(RoomType roomType)
        {
            _dbContext.RoomTypes.Add(roomType);
        }

        public async Task<List<RoomType>> GetAllAsync()
        {
            return await _dbContext.RoomTypes.ToListAsync();
        }

        public async Task<RoomType?> GetByIdAsync(int id)
        {
            return await _dbContext.RoomTypes.Include(rt => rt.Rooms).SingleOrDefaultAsync(rt => rt.Id == id);
        }

        public void Delete(RoomType roomType)
        {
            _dbContext.RoomTypes.Remove(roomType);
        }

        public async Task<List<RoomType>> GetAvailableAsync(int? capacity, decimal? maxPrice, DateTime requestedCheckIn, DateTime requestedCheckOut)
        {
            requestedCheckIn = DateTime.SpecifyKind(requestedCheckIn, DateTimeKind.Utc);
            requestedCheckOut = DateTime.SpecifyKind(requestedCheckOut, DateTimeKind.Utc);

            var query = _dbContext.RoomTypes
            .Where(rt =>
                _dbContext.Bookings.Count(b =>
                    b.RoomType.Id == rt.Id &&
                    b.CheckInDate < requestedCheckOut &&
                    b.CheckOutDate > requestedCheckIn
                ) < rt.Rooms.Count()
            );

            if (capacity.HasValue && capacity.Value != 0)
                query = query.Where(r => r.Capacity == capacity.Value);

            if (maxPrice.HasValue && maxPrice.Value != 0)
                query = query.Where(r => r.PricePerNight <= maxPrice.Value);

            return await query.ToListAsync();
        }

        public async Task<int> GetOverlappingAsync(int roomTypeId, DateTime checkInDate, DateTime checkOutDate)
        {
            return await _dbContext.Bookings.CountAsync(b =>
                b.RoomType.Id == roomTypeId &&
                b.CheckInDate < checkOutDate &&
                b.CheckOutDate > checkInDate
            );
        }

        public async Task<int> RoomCountAsync(int roomTypeId)
        {
            return await _dbContext.Rooms.Include(r => r.RoomType).CountAsync(r => r.RoomType.Id == roomTypeId);
        }
    }
}
