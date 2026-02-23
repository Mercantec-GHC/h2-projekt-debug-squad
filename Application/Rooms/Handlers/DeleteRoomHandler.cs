using Application.Interfaces;
using Domain;

namespace Application.Rooms.Handlers
{
    public class DeleteRoomHandler
    {
        private readonly IRoomRepository _repository;

        public DeleteRoomHandler(IRoomRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(int id)
        {
            Room? room = await _repository.GetByIdAsync(id) ?? throw new ArgumentException("Room not found.");
            _repository.Delete(room);

            await _repository.SaveChangesAsync();
        }
    }
}
