using Domain;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IBookingRepository
    {
        Task AddAsync(Booking booking, int guestId);
    }
}
