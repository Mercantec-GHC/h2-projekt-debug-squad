using Domain;

namespace Application.Interfaces
{
    public interface IBookingRepository
    {
        Task SaveChangesAsync();
        Task<List<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id);
    }
}
