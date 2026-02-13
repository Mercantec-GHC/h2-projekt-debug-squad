using Application.Interfaces;
using Application.Bookings.Commands;
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

        public async Task<bool> Handle(DeleteBookingCommand command)
        {
            Guest guest = await _guestRepository.GetByIdAsync(command.GuestId);

            Booking? booking = guest.Bookings
                .FirstOrDefault(x => x.Id == command.BookingId);

            if (booking is null)
                return false;

            guest.RemoveBooking(booking);

            await _guestRepository.SaveChangesAsync();
            return true;
        }
    }
}
