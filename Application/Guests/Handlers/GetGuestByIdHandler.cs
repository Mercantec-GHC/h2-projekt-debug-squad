using Application.Interfaces;
using Application.Guests.Queries;
using Domain;

namespace Application.Guests.Handlers
{
        public class GetGuestByIdHandler
        {
            private readonly IGuestRepository _repository;

            public GetGuestByIdHandler(IGuestRepository repository)
            {
                _repository = repository;
            }

            public async Task<Guest> Handle(GuestByIdQuery query)
            {
                return await _repository.GetByIdAsync(query.Id);
            }
        }
}
