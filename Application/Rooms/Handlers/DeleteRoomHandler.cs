using Application.Interfaces;

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
            await _repository.DeleteByIdAsync(id);
        }
    }
}
