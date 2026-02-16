namespace Application.Guests.Queries
{
    public class GuestByIdQuery
    {
        public int Id { get; set; }

        public GuestByIdQuery(int id) { Id = id; }
    }
}
