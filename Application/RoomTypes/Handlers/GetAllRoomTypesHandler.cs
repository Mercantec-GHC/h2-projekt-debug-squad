using Application.Interfaces;
using Domain;
using Shared;

namespace Application.RoomTypes.Handlers
{
    public class GetAllRoomTypesHandler
    {
        private readonly IRoomTypeRepository _repository;

        public GetAllRoomTypesHandler(IRoomTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RoomTypeDto>> Handle()
        {
            List<RoomType> roomTypes = await _repository.GetAllAsync();

            return roomTypes.Select(rt => new RoomTypeDto(rt.Id, rt.Name, rt.Capacity, rt.PricePerNight)).ToList();
        }
    }
}
