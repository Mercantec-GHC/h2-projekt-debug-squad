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
            var room = new Room(command.Number, command.Capacity, command.PricePerNight, command.IsAvailable);

            await _repository.AddAsync(room);
        }
    }
}
