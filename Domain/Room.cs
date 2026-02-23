namespace Domain
{
    public class Room
    {
        public int Id { get; private set; }
        public string Number { get; private set; } = string.Empty;
        public RoomType RoomType { get; private set; } = null!;

        private Room() { }

        public Room(string number, RoomType roomType)
        {
            if (string.IsNullOrWhiteSpace(number)) throw new ArgumentException("Room number is required");
            Number = number;
            RoomType = roomType ?? throw new ArgumentException("Room type is required");
        }

        public void ChangeRoomType(RoomType roomType)
        {
            RoomType = roomType ?? throw new ArgumentException("Room type is required");
        }
    }
}
