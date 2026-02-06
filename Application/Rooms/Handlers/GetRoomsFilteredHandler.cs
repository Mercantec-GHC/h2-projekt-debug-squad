using Application.Interfaces;
using Application.Rooms.Queries;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

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
            List<Room> rooms = await _repository.GetFilteredAsync(orderBy, roomAmount,
            showOnlyAvailable, orderDescending);

            return rooms.Select(room => new RoomDto(room.Id, room.Number, room.Capacity, room.PricePerNight, room.IsAvailable)).ToList();
        }
    }
}
