using Application.Interfaces;
using Domain;

namespace Application.Rooms.Handlers
{
    public class DeleteRoomHandler
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;

        public DeleteRoomHandler(IRoomRepository roomRepository, IBookingRepository bookingRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task Handle(int id)
        {
            Room? room = await _roomRepository.GetByIdAsync(id) ?? throw new ArgumentException("Room not found.");

            var bookings = await _bookingRepository.GetAllAsync();
            bool hasBookings = bookings.Any(b => b.Room.Id == id);

            if (hasBookings) 
                throw new InvalidOperationException("Cannot delete a room that has bookings.");

            _roomRepository.Delete(room);

            await _roomRepository.SaveChangesAsync();
        }
    }
}
