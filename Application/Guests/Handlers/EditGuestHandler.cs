using Application.Guests.Commands;
using Application.Interfaces;
using Domain;

namespace Application.Guests.Handlers
{
    public class EditGuestHandler
    {
        private readonly IGuestRepository _repository;

        public EditGuestHandler(IGuestRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(EditGuestCommand command)
        {
            Guest? guest = await _repository.GetByIdAsync(command.Id);

            if (guest == null) throw new ArgumentException("Guest not found");

            guest.Change(command.FullName, command.PhoneNumber, command.Email);
            await _repository.SaveChangesAsync();
        }
    }
}
