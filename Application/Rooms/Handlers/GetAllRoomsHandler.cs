using Application.Interfaces;
using Application.Rooms.Queries;
using Domain;

namespace Application.Rooms.Handlers
{
    public class GetAllRoomsHandler
    {
        private readonly IRoomRepository _repository;

        public GetAllRoomsHandler(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RoomDto>> Handle()
        {
            List<Room> rooms = await _repository.GetAllAsync();

            return rooms.Select(room => new RoomDto(room.Id, room.Number, room.Capacity, room.PricePerNight)).ToList();
        }
    }
}
