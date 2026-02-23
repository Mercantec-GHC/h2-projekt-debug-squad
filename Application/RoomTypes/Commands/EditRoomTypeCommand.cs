using System;
using System.Collections.Generic;
using System.Text;

namespace Application.RoomTypes.Commands
{
    public class EditRoomTypeCommand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
