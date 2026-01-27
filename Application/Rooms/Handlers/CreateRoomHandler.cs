using Application.Interfaces;
using Application.Rooms.Commands;
using Domain;

namespace Application.Rooms.Handlers
{
    // Application layer handler / use case for creating a Room.
    // In Clean Architecture / DDD, each use case has its own handler or service class.
    // The handler coordinates the operation: it takes input, uses the domain, and calls the repository.
    public class CreateRoomHandler
    {
        // Repository abstraction for Room entity.
        // The handler depends on this interface, not the concrete database implementation.
        private readonly IRoomRepository _repository;

        // Constructor injection of the repository.
        // Ensures the handler can save the Room entity without knowing how it's stored.
        public CreateRoomHandler(IRoomRepository repository)
        {
            _repository = repository;
        }

        // Handles the "Create Room" command.
        // This method:
        // 1. Transforms input data (command) into a Domain entity.
        // 2. Delegates persistence to the repository.
        // Note: Business validation happens inside the Room entity, not here.
        public async Task Handle(CreateRoomCommand command)
        {
            // 1️. Create a new Room entity in the Domain.
            var room = new Room(command.Number, command.Capacity, command.PricePerNight);

            // 2️. Persist the new Room via the repository abstraction.
            await _repository.AddAsync(room);
        }
    }
}
