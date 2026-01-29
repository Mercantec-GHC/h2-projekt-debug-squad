using Application.Interfaces;
using Application.Rooms.Commands;
using Domain;

namespace Application.Rooms.Handlers
{
    public class CreateRoomHandler
    {
        private readonly IRoomRepository _repository;

        public CreateRoomHandler(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateRoomCommand command)
        {
            // 1️. Create a new Room entity in the Domain.
            var room = new Room(command.Number, command.Capacity, command.PricePerNight);

            // 2️. Persist the new Room via the repository abstraction.
            await _repository.AddAsync(room);
        }
    }
}
