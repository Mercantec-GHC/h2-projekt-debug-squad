using Application.Interfaces;
using Domain;
using Shared;

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
            Room? room = await _repository.GetByIdAsync(roomDto.Id);

            if (room == null)
                throw new Exception("Room not found.");

            room.Change(roomDto.Number, roomDto.Capacity, roomDto.PricePerNight);
            await _repository.SaveChangesAsync();
        }
    }
}
