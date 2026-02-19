using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Rooms.Commands
{
    public class EditRoomCommand
    {
        public int RoomId { get; set; }
        public int RoomTypeId { get; set; }
    }
}
