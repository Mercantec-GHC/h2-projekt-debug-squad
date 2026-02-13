namespace Application.Rooms.Queries
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }

        private RoomDto() { }

        public RoomDto(int id, string number, int capacity, decimal pricePerNight, bool isAvailable)
        {
            Id = id;
            Number = number;
            Capacity = capacity;
            PricePerNight = pricePerNight;
            IsAvailable = isAvailable;
        }
    }
}
