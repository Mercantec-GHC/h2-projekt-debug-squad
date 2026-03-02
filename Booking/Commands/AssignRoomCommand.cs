using Application.Interfaces;
using Domain;
using Shared;

namespace Application.Rooms.Handlers
{
    public class GetAvailableRoomsHandler
    {
        private readonly IRoomRepository _roomRepository;

        public GetAvailableRoomsHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<RoomDto>> Handle(GetAvailableRoomsCommand command)
        {
            // Validate command parameters
            if (command.CheckInDate >= command.CheckOutDate)
                throw new ArgumentException("Check-in date must be earlier than check-out date.");
            if (command.Capacity <= 0)
                throw new ArgumentException("Capacity must be greater than zero.");
            if (command.MaxPrice <= 0)
                throw new ArgumentException("Max price must be greater than zero.");

            try
            {
                // Fetch available rooms
                var rooms = await _roomRepository.GetAvailableAsync(command.CheckInDate, command.CheckOutDate, command.Capacity, command.MaxPrice);
                if (rooms == null || !rooms.Any())
                    return new List<RoomDto>();

                // Map and sort rooms
                return rooms
                    .OrderBy(r => r.Number)
                    .Select(r => new RoomDto(r.Id, r.Number, new RoomTypeDto(r.RoomType.Id, r.RoomType.Name, r.RoomType.Capacity, r.RoomType.PricePerNight)))
                    .ToList();
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                throw new ApplicationException("An error occurred while fetching available rooms.", ex);
            }
        }
    }
}