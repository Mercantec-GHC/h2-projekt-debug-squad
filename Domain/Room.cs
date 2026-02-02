namespace Domain
{
    public class Room
    {
        public int Id { get; private set; }

        public string Number { get; private set; } = string.Empty;

        public int Capacity { get; private set; }

        public decimal PricePerNight { get; private set; }

        private Room() { }

        public Room(string number, int capacity, decimal pricePerNight)
        {
            Validate(number, capacity, pricePerNight);

            Number = number;
            Capacity = capacity;
            PricePerNight = pricePerNight;
        }

        private static void Validate(string number, int capacity, decimal pricePerNight)
        {
            if (string.IsNullOrWhiteSpace(number)) throw new ArgumentException("Room number is required");

            if (capacity <= 0) throw new ArgumentException("Capacity must be greater than 0");

            if (pricePerNight <= 0) throw new ArgumentException("Price must be greater than 0");
        }
    }
}
