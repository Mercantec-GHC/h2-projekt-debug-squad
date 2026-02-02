namespace Application.Rooms.Commands
{
    public class CreateRoomCommand
    {
        public string Number { get; init; } = string.Empty;
        public int Capacity { get; init; }
        public decimal PricePerNight { get; init; }
        public bool IsAvailable { get; init; }
    }
}
