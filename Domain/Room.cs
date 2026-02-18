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
            Validate(number);

            Number = number;
            RoomType = roomType;
        }

        private static void Validate(string number)
        {
            if (string.IsNullOrWhiteSpace(number)) throw new ArgumentException("Room number is required");
        }
    }
}
