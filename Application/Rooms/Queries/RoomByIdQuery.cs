namespace Application.Rooms.Queries
{
    public class RoomByIdQuery
    {
        public int Id { get; set; }

        public RoomByIdQuery(int id) { Id = id; }
    }
}
