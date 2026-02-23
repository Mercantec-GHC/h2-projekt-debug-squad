using Application.Bookings.Commands;
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

        public async Task Handle(DeleteBookingCommand command)
        {
            var guest = await _guestRepository.GetByIdAsync(command.GuestId) ?? throw new ArgumentException("Guest not found");

            var booking = guest.Bookings.FirstOrDefault(x => x.Id == command.BookingId) ?? throw new ArgumentException("Booking not found");

            guest.RemoveBooking(booking);

            await _guestRepository.SaveChangesAsync();
        }
    }
}
