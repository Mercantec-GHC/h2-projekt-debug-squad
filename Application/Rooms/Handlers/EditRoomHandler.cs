using Application.Interfaces;
using Application.Rooms.Commands;
using Domain;
using Shared;

namespace Application.Rooms.Handlers
{
    public class EditRoomHandler
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        public EditRoomHandler(IRoomRepository roomRepository, IRoomTypeRepository roomTypeRepository)
        {
            _roomRepository = roomRepository;
            _roomTypeRepository = roomTypeRepository;
        }

        public async Task Handle(EditRoomCommand command)
        {
            Room? room = await _roomRepository.GetByIdAsync(command.RoomId);

            RoomType? roomType = await _roomTypeRepository.GetByIdAsync(command.RoomTypeId);

            if (room == null)
                throw new Exception("Room not found.");
            if (roomType == null)
                throw new Exception("RoomType not found");

            room.ChangeRoomType(roomType);
            await _roomRepository.SaveChangesAsync();
        }
    }
}
