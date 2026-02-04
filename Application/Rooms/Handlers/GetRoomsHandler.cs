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

        public async Task<IReadOnlyList<Room>> Handle()
        {
            return await _repository.GetAllAsync();
        }
    }
}
