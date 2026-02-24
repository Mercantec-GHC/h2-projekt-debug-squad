namespace Shared
{
    public class CreateRoomCommand
    {
        public string Number { get; set; } = string.Empty;
        public int RoomTypeId { get; set; }

        public CreateRoomCommand() { }
        public CreateRoomCommand(string number, int roomTypeId)
        {
            Number = number;
            RoomTypeId = roomTypeId;
        }
    }
}
