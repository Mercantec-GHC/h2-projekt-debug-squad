namespace Domain
{
    public class RoomType
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int Capacity { get; private set; }
        public decimal PricePerNight { get; private set; }
        public List<Room> Rooms { get; private set; } = new();

        private RoomType() { }
        public RoomType(string name, int capacity, decimal pricePerNight)
        {
            Validate(name, capacity, pricePerNight);

            Name = name;
            Capacity = capacity;
            PricePerNight = pricePerNight;
        }

        public void AddRoom(Room room)
        {
            Rooms.Add(room);
        }

        public void RemoveRoom(Room room)
        {
            Rooms.Remove(room);
        }

        public void Change(int capacity, decimal pricePerNight)
        {
            Capacity = capacity;
            PricePerNight = pricePerNight;
        }

        private static void Validate(string name, int capacity, decimal pricePerNight)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Room type name is required");

            if (capacity <= 0) throw new ArgumentException("Capacity must be greater than 0");

            if (pricePerNight <= 0) throw new ArgumentException("Price must be greater than 0");
        }
    }
}
