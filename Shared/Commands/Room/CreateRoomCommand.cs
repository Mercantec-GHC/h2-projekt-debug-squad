namespace Shared
{
    public class CreateRoomCommand
    {
        public string Number { get; init; } = string.Empty;
        public int RoomTypeId { get; init; }
    }
}
