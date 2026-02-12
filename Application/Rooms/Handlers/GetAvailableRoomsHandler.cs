using Application.Interfaces;
using Application.Rooms.Commands;
using Application.Rooms.Queries;
using Domain;

namespace Application.Rooms.Handlers
{
    public class GetAvailableRoomsHandler
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;

        public GetAvailableRoomsHandler(IRoomRepository roomRepository, IBookingRepository bookingRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<List<RoomDto>> Handle(GetAvailableRoomsCommand command)
        {
            DateTime requestedCheckIn = command.CheckInDate.Date;
            DateTime requestedCheckOut = command.CheckOutDate.Date;

            var rooms = await _roomRepository.GetAllAsync();
            List<Booking> bookings = await _bookingRepository.GetAllAsync();

            var bookedRoomIds = bookings
                .Where(b =>
                    b.CheckInDate < requestedCheckOut &&
                    b.CheckOutDate > requestedCheckIn)
                .Select(b => b.Room.Id)
                .Distinct()
                .ToList();

            var availableRooms = rooms
                .Where(r => !bookedRoomIds.Contains(r.Id))
                .Select(r => new RoomDto(
                    r.Id, 
                    r.Number, 
                    r.Capacity, 
                    r.PricePerNight, 
                    r.IsAvailable
                    )).ToList();

            return availableRooms;
        }
    }
}
