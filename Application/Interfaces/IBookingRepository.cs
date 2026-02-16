using Domain;

namespace Application.Interfaces
{
    public interface IBookingRepository
    {
        //Task AddAsync(Booking booking, int guestId);
        Task SaveChangesAsync();
        Task<List<Booking>> GetAllAsync();

        Task<Booking>? GetByIdAsync(int id);
    }
}
