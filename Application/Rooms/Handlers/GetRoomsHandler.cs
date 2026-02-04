using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Application.Rooms.Queries;

namespace Application.Rooms.Handlers
{
    public class GetRoomsHandler
    {
        private readonly IRoomRepository _repository;

        public GetRoomsHandler(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RoomDto>> Handle()
        {
            List<Room> rooms = await _repository.GetAllAsync();

            return rooms.Select(room => new RoomDto(room.Id, room.Number, room.Capacity, room.PricePerNight, room.IsAvailable)).ToList();
        }
    }
}
