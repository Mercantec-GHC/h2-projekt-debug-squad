namespace Application.Rooms.Commands
{
    public class CreateRoomCommand
    {
        public string Number { get; init; } = string.Empty;
        public int RoomTypeId { get; init; }
    }
}
