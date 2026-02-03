using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Rooms.Queries
{
    public class RoomByIdQuery
    {
        public int Id { get; set; }

        public RoomByIdQuery(int id) { Id = id; }
    }
}
