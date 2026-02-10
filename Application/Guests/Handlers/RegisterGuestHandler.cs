using Application.Interfaces;  // use repository interface
using Domain;
using Application.Guests.Commands;

namespace Application.Guests.Handlers
{
    public class RegisterGuestHandler
    {
        private readonly IGuestRepository _guestRepository;

        public RegisterGuestHandler(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task Handle(RegisterGuestCommand command)
        {
            var guest = new Guest(command.FullName, command.PhoneNumber, command.Email);
            await _guestRepository.AddAsync(guest);
        }
    }
}
