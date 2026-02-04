using Application.Interfaces;
using Application.Rooms.Queries;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Rooms.Handlers
{
    public class GetRoomByIdHandler
    {
        private readonly IRoomRepository _repository;

        public GetRoomByIdHandler(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task<RoomDto> Handle(int id)
        {
            Room room = await _repository.GetByIdAsync(id);
            if (room == null)
            {
                return null;
            }

            return new RoomDto(room.Id, room.Number, room.Capacity, room.PricePerNight, room.IsAvailable);
        }
    }
}
