using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Rooms.Queries
{
    public class GetRoomByIdQuery
    {
        public int Id { get; set; }

        public GetRoomByIdQuery(int id) { Id = id; }
    }
}
