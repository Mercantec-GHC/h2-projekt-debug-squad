using Application.Interfaces;
using Domain;


namespace Application.Bookings.Handlers
{
    public class DeleteBookingHandler
    {
        private readonly IGuestRepository _guestRepository;

        public DeleteBookingHandler(IGuestRepository repository)
        {
            _guestRepository = repository;
        }

        public async Task<bool> Handle(int guestId, int bookingId)
        {
            Guest guest = await _guestRepository.GetByIdAsync(guestId);

            Booking? booking = guest.Bookings.FirstOrDefault(x => x.Id == bookingId);

            if (booking is null)
                return false;

            guest.RemoveBooking(booking);

            await _guestRepository.SaveChangesAsync();
            return true;
        }
    }
}
