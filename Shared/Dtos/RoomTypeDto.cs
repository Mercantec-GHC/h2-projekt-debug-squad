namespace Shared
{
    public class RoomTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }

        public RoomTypeDto() { }

        public RoomTypeDto(int id, string name, int capacity, decimal pricePerNight)
        {
            Id = id;
            Name = name;
            Capacity = capacity;
            PricePerNight = pricePerNight;
        }

        public RoomTypeDto(int id)
        {
            Id = id; 
        }
    }
}
