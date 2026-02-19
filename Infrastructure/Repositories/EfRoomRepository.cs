using Application.Interfaces;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            return await _dbContext.Rooms
                .Include(r => r.RoomType)
                .ToListAsync();
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            return await _dbContext.Rooms
                .Include(r => r.RoomType)
                .SingleOrDefaultAsync(r => r.Id == id);
        }

        public void Delete(Room room)
        {
            _dbContext.Rooms.Remove(room);
        }

        public async Task<List<Room>> GetAvailableAsync(
            DateTime requestedCheckIn,
            DateTime requestedCheckOut,
            int roomTypeId)
        {
            // Get IDs of rooms that are already booked in the requested period
            var bookedRoomIds = await _dbContext.Bookings
                .Where(b => b.CheckInDate < requestedCheckOut &&
                            b.CheckOutDate > requestedCheckIn)
                .Select(b => b.Id) // Use FK instead of navigation property
                .Distinct()
                .ToListAsync();

            // Get rooms of the requested type that are NOT booked
            var availableRooms = await _dbContext.Rooms
                .Where(r => r.RoomType.Id == roomTypeId &&
                            !bookedRoomIds.Contains(r.Id))
                .Include(r => r.RoomType) // optional
                .ToListAsync();

            return availableRooms;
        }


        //public async Task<List<Room>> GetFilteredAsync(
        //    Expression<Func<Room, object>> orderBy,
        //    int roomAmount = 50,
        //    bool showOnlyAvailable = true,
        //    bool orderDescending = true)
        //{
        //    var query = _dbContext.Rooms
        //        .Where(r => r.IsAvailable == showOnlyAvailable).Take(roomAmount);

        //    query = orderDescending
        //        ? query.OrderByDescending(orderBy)
        //        : query.OrderBy(orderBy);

        //    return await query.ToListAsync();
        //}
    }
}
