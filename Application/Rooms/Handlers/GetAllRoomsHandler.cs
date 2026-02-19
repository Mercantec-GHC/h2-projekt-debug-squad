using Application.Interfaces;
using Domain;
using Shared;

namespace Application.Rooms.Handlers
{
    public class GetAllRoomsHandler
    {
        private readonly IRoomRepository _roomRepository;

        public GetAllRoomsHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<RoomDto>?> Handle()
        {
            List<Room> rooms = await _roomRepository.GetAllAsync();

            return rooms.Select(room => new RoomDto(
                room.Id,
                room.Number,
                new RoomTypeDto(
                    room.RoomType.Id,
                    room.RoomType.Name,
                    room.RoomType.Capacity,
                    room.RoomType.PricePerNight
                    )
            )).ToList();
        }
    }
}
