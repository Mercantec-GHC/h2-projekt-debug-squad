using Application.Interfaces;
using Domain;

namespace Application.Guests.Handlers
{
    public class DeleteGuestHandler
    {
        private readonly IGuestRepository _repository;

        public DeleteGuestHandler(IGuestRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(int guestId)
        {
            Guest? guest = await _repository.GetByIdAsync(guestId);

            if (guest == null) throw new ArgumentException("Guest not found.");

            _repository.Delete(guest);
            await _repository.SaveChangesAsync();
        }
    }
}
