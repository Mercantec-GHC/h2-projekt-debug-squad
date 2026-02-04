using Application.Interfaces;
using Application.Guests.Queries;

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
            // Calls repository to delete by Id
            await _repository.DeleteByIdAsync(guestId);
        }
    }
}
