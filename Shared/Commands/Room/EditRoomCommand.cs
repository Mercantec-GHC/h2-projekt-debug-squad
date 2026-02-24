namespace Shared
{
    public class EditRoomCommand
    {
        public int RoomId { get; set; }
        public int RoomTypeId { get; set; }

        public EditRoomCommand() { }
        public EditRoomCommand(int roomId, int roomTypeId)
        {
            RoomId = roomId;
            RoomTypeId = roomTypeId;
        }
    }
}
