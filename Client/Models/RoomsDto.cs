namespace Client.Models
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
    }
}
