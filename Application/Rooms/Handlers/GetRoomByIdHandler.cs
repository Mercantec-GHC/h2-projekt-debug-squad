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

        public async Task<Room> Handle(GetRoomByIdQuery query)
        {
            return await _repository.GetByIdAsync(query.Id);
        }
    }
}
