using Application.Interfaces;
using Domain;
using Application.Guests.Commands;


namespace Application.Guests.Handlers
{
    public class AddGuestHandler
    {
        private readonly IGuestRepository _repository;

        public AddGuestHandler(IGuestRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(string fullName, string phoneNumber, string email)
        {
            var guest = new Guest(fullName, phoneNumber, email);

            await _repository.AddAsync(guest);
        }
    }
}
