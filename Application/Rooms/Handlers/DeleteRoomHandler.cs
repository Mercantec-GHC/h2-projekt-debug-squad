using Application.Interfaces;
using Application.Rooms.Commands;
using Application.Rooms.Queries;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Rooms.Handlers
{
    public class DeleteRoomHandler
    {
        private readonly IRoomRepository _repository;

        public DeleteRoomHandler(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(RoomByIdQuery query)
        {
            await _repository.DeleteByIdAsync(query.Id);
        }
    }
}
