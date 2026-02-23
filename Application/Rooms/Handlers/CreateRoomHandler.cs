using Application.Interfaces;
using Domain;
using Shared;

namespace Application.Rooms.Handlers
{
    public class CreateRoomHandler
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;

        public CreateRoomHandler(IRoomRepository roomRepository, IRoomTypeRepository roomTypeRepository)
        {
            _roomRepository = roomRepository;
            _roomTypeRepository = roomTypeRepository;
        }

        public async Task Handle(CreateRoomCommand command)
        {
            var roomType = await _roomTypeRepository.GetByIdAsync(command.RoomTypeId);
            if (roomType == null) throw new ArgumentException("Room type not found");

            var room = new Room(command.Number, roomType);

            _roomRepository.Add(room);
            await _roomRepository.SaveChangesAsync();
        }
    }
}
