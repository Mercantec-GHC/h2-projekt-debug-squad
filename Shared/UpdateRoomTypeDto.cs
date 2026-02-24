using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class UpdateRoomTypeDto
    {
        public int RoomId { get; set; }
        public int RoomTypeId { get; set; }

        public UpdateRoomTypeDto() { }
        public UpdateRoomTypeDto(int roomId, int roomTypeId)
        {
            RoomId = roomId;
            RoomTypeId = roomTypeId;
        }
    }
}
