using Application.Interfaces;
using Application.Rooms.Commands;
using Application.Rooms.Queries;
using Domain;

namespace Application.Rooms.Handlers
{
    public class EditRoomHandler
    {
        private readonly IRoomRepository _repository;

        public EditRoomHandler(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(RoomDto roomDto)
        {
            // kan vi måske prøve at hente den eksisterende room og arbejde med den...
            // var room = await _repository.GetByIdAsync(roomDto.Id); bla-bla-bla

            var room = new Room(roomDto.Number, roomDto.Capacity, roomDto.PricePerNight, roomDto.IsAvailable);

            await _repository.EditAsync(room, roomDto.Id);
        }
    }
}
