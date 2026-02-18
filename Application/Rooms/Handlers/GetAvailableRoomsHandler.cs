using Application.Interfaces;
using Application.Rooms.Commands;
using Domain;
using Shared;

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

        public async Task<List<RoomDto>?> Handle(GetAvailableRoomsCommand command)
        {
            var requestedCheckIn = command.CheckInDate.Date;
            var requestedCheckOut = command.CheckOutDate.Date;

            var rooms = await _roomRepository.GetAllAsync();
            var bookings = await _bookingRepository.GetAllAsync();

            var bookedRoomIds = bookings
                .Where(b => b.CheckInDate.Date < requestedCheckOut &&
                            b.CheckOutDate.Date > requestedCheckIn)
                .Select(b => b.Room.Id)
                .Distinct()
                .ToList();

            var availableRooms = rooms
                .Where(r => !bookedRoomIds.Contains(r.Id));

            if (command.Capacity.HasValue)
                availableRooms = availableRooms.Where(r => r.RoomType.Capacity >= command.Capacity.Value);

            if (command.MaxPrice.HasValue)
                availableRooms = availableRooms.Where(r => r.RoomType.PricePerNight <= command.MaxPrice.Value);

            return availableRooms
                .Select(r => new RoomDto(
                    r.Id,
                    r.Number,
                    r.RoomType.Id,
                    r.RoomType.Name,
                    r.RoomType.Capacity,
                    r.RoomType.PricePerNight
                ))
                .ToList();
        }
    }
}

