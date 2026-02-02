using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Rooms.Handlers
{
    public class GetRoomsHandler
    {
        private readonly IRoomRepository _repository;

        public GetRoomsHandler(IRoomRepository repository)
        {
            _repository = repository;
        }




    }
}
