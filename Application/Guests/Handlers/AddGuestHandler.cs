using Application.Interfaces;
using Application.Guests.Commands;
using Domain;

namespace Application.Guests.Handlers
{
    public class AddGuestHandler
    {
        private readonly IGuestRepository _repository;

        public AddGuestHandler(IGuestRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(AddGuestCommand command)
        {
            if (await _repository.ExistsByEmailAsync(command.Email))
                throw new InvalidOperationException("Guest with this email already exists");

            var guest = new Guest(
                command.FullName,
                command.PhoneNumber,
                command.Email
            );

            await _repository.AddAsync(guest);
        }
    }
}
