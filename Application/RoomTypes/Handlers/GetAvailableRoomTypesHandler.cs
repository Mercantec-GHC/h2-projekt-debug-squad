using Application.Interfaces;
using Domain;
using Shared;

namespace Application.RoomTypes.Handlers
{
    public class GetAvailableRoomTypesHandler
    {
        private readonly IRoomTypeRepository _repository;

        public GetAvailableRoomTypesHandler(IRoomTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RoomTypeDto>> Handle(GetAvailableRoomTypesCommand command)
        {
            List<RoomType> roomTypes = await _repository.GetAvailableAsync(command.Capacity, command.MaxPrice, command.CheckInDate, command.CheckOutDate);

            return roomTypes.Select(rt => new RoomTypeDto(rt.Id, rt.Name, rt.Capacity, rt.PricePerNight)).ToList();
        }
    }
}
