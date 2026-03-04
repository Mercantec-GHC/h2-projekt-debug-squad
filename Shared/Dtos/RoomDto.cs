namespace Shared
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public RoomTypeDto? RoomType { get; set; }
        public RoomDto() { }

        public RoomDto(int id, string number, RoomTypeDto roomType)
        {
            Id = id;
            Number = number;
            RoomType = roomType;
        }

        public RoomDto(int id, string number)
        {
            Id = id;
            Number = number;
        }
    }
}
