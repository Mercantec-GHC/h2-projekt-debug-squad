using Application.Interfaces;
using Domain;
using Shared;

namespace Application.Rooms.Handlers
{
    public class GetRoomsFilteredHandler
    {
        private readonly IRoomRepository _repository;

        public GetRoomsFilteredHandler(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RoomDto>> Handle()
        {
            List<Room> rooms = await _repository.GetAllAsync();

            return rooms.Select(room => new RoomDto(
                room.Id,
                room.Number,
                room.RoomType.Id,
                room.RoomType.Name,
                room.RoomType.Capacity,
                room.RoomType.PricePerNight
            )).ToList();
        }
    }
}
