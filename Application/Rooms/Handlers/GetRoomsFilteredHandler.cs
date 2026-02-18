using Application.Interfaces;
using Domain;
using Shared;
using System.Linq.Expressions;

namespace Application.Rooms.Handlers
{
    public class GetRoomsFilteredHandler
    {
        private readonly IRoomRepository _repository;

        public GetRoomsFilteredHandler(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RoomDto>> Handle(
            Expression<Func<Room, object>> orderBy,
            int roomAmount,
            bool showOnlyAvailable,
            bool orderDescending
            )
        {
            //List<Room> rooms = await _repository.GetFilteredAsync(orderBy, roomAmount,
            //showOnlyAvailable, orderDescending);

            //return rooms.Select(room => new RoomDto(room.Id, room.Number, room.Capacity, room.PricePerNight)).ToList();
            return null;
        }
    }
}
