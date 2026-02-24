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

        public async Task<List<Room>> GetAvailableAsync(DateTime requestedCheckIn, DateTime requestedCheckOut, int? capacity, decimal? maxPrice)
        {
            requestedCheckIn = DateTime.SpecifyKind(requestedCheckIn, DateTimeKind.Utc);
            requestedCheckOut = DateTime.SpecifyKind(requestedCheckOut, DateTimeKind.Utc);

            var bookedRoomIds = await _dbContext.Bookings.Where(b => b.CheckInDate < requestedCheckOut && b.CheckOutDate > requestedCheckIn).Select(b => b.Room.Id).Distinct().ToListAsync();
            var availableRooms = await _dbContext.Rooms.Where(r => !bookedRoomIds.Contains(r.Id)).Include(r => r.RoomType).ToListAsync();

            if (capacity.HasValue && capacity.Value != 0) availableRooms = availableRooms.Where(r => r.RoomType.Capacity == capacity).ToList();
            if (maxPrice.HasValue && maxPrice.Value != 0) availableRooms = availableRooms.Where(r => r.RoomType.PricePerNight <= maxPrice).ToList();

            return availableRooms;
        }
    }
}
