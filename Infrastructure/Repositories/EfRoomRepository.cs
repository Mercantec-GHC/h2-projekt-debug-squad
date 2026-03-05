using Application.Interfaces;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Repositories
{
    public class EfRoomRepository : IRoomRepository
    {
        private readonly AppDbContext _dbContext;

        public EfRoomRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Add(Room room)
        {
            _dbContext.Rooms.Add(room);
        }

        public async Task<List<Room>> GetAllAsync()
        {
            return await _dbContext.Rooms.Include(r => r.RoomType).ToListAsync();
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            return await _dbContext.Rooms.Include(r => r.RoomType).SingleOrDefaultAsync(r => r.Id == id);
        }

        public void Delete(Room room)
        {
            _dbContext.Rooms.Remove(room);
        }

        public async Task<List<Room>> GetAvailableAsync(
          DateTime requestedCheckIn,
          DateTime requestedCheckOut,
          int? capacity = null,
          decimal? maxPrice = null,
          int? excludeRoomId = null // new optional parameter
         )
        {
            // Make sure dates are treated as UTC
            requestedCheckIn = DateTime.SpecifyKind(requestedCheckIn, DateTimeKind.Utc);
            requestedCheckOut = DateTime.SpecifyKind(requestedCheckOut, DateTimeKind.Utc);

            // Get IDs of rooms that are already booked during the requested period
            var bookedRoomIds = await _dbContext.Bookings
                .Where(b => b.CheckInDate < requestedCheckOut &&
                            b.CheckOutDate > requestedCheckIn &&
                            b.Room != null)
                .Select(b => b.Room!.Id)
                .Distinct()
                .ToListAsync();

            // Start with all rooms that are not booked
            var query = _dbContext.Rooms
                .Include(r => r.RoomType)
                .Where(r => !bookedRoomIds.Contains(r.Id));

            // Exclude the currently assigned room (for reassignment)
            if (excludeRoomId.HasValue)
            {
                query = query.Where(r => r.Id != excludeRoomId.Value);
            }

            // Apply capacity filter if provided
            if (capacity.HasValue && capacity.Value != 0)
            {
                query = query.Where(r => r.RoomType.Capacity == capacity.Value);
            }

            // Apply max price filter if provided
            if (maxPrice.HasValue && maxPrice.Value != 0)
            {
                query = query.Where(r => r.RoomType.PricePerNight <= maxPrice.Value);
            }

            return await query.ToListAsync();
        }
    }
}
