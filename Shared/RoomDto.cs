namespace Shared
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }

        public RoomDto() { }

        public RoomDto(int id, string number, int capacity, decimal pricePerNight)
        {
            Id = id;
            Number = number;
            Capacity = capacity;
            PricePerNight = pricePerNight;
        }
    }
}
