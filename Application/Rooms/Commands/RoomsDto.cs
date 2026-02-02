namespace Application.Rooms.Commands
{
    public class RoomDto
    {
        public int Id { get; private set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
    }
}
