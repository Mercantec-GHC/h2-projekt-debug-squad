using Application.Interfaces;  // <-- use repository interface
using Domain;
using Application.Guests.Commands;

namespace Application.Guests.Handlers
{
    public class RegisterGuestHandler
    {
        private readonly IGuestRepository _repository;

        public RegisterGuestHandler(IGuestRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(RegisterGuestCommand command)
        {
            var guest = new Guest(command.FullName, command.PhoneNumber, command.Email);

            guest.SetPassword(command.Password);

            await _repository.AddAsync(guest); // uses repository, no DbContext here
        }
    }
}
