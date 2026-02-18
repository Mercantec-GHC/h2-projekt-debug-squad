namespace Shared
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }

        public RoomDto() { }

        public RoomDto(int id, string number, int roomTypeId, string roomTypeName, int capacity, decimal pricePerNight)
        {
            Id = id;
            Number = number;
            RoomTypeId = roomTypeId;
            RoomTypeName = roomTypeName;
            Capacity = capacity;
            PricePerNight = pricePerNight;
        }
    }
}
