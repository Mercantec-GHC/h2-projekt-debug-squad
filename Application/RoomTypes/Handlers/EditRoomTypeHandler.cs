using Application.Interfaces;
using Domain;
using Shared;

namespace Application.RoomTypes.Handlers
{
    public class EditRoomTypeHandler
    {
        private readonly IRoomTypeRepository _repository;

        public EditRoomTypeHandler(IRoomTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(EditRoomTypeCommand command)
        {
            RoomType? roomType = await _repository.GetByIdAsync(command.Id);

            if (roomType == null) throw new ArgumentException("The roomType id is invalid");

            roomType.Change(command.Name, command.Capacity, command.PricePerNight);

            await _repository.SaveChangesAsync();
        }
    }
}
