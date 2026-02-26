using Application.Interfaces;
using Domain;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.RoomTypes.Handlers
{
    public class GetRoomTypeByIdHandler
    {
        private readonly IRoomTypeRepository _repository;

        public GetRoomTypeByIdHandler(IRoomTypeRepository repository)
        {
            _repository = repository;
        }
        //int id, string name, int capacity, decimal pricePerNight
        public async Task<RoomTypeDto?> Handle(int id)
        {
            RoomType? roomType = await _repository.GetByIdAsync(id);
            if (roomType == null) return null;

            return new RoomTypeDto(roomType.Id, roomType.Name, roomType.Capacity, roomType.PricePerNight);
        }
    }
}
