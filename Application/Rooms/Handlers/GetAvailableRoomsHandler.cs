using Application.Interfaces;
using Application.Rooms.Commands;
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

        public async Task<List<RoomDto>?> Handle(GetAvailableRoomsCommand command)
        {
            var rooms = await _roomRepository.GetAvailableAsync(command.CheckInDate, command.CheckOutDate, command.Capacity, command.MaxPrice);

            return rooms.Select(r => new RoomDto(r.Id, r.Number, new RoomTypeDto(r.RoomType.Id, r.RoomType.Name, r.RoomType.Capacity, r.RoomType.PricePerNight))).ToList();
        }
    }
}

