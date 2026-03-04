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

        public async Task<List<RoomTypeDto>> Handle(bool includeRooms = false)
        {
            List<RoomType> roomTypes = await _repository.GetAllAsync(includeRooms: includeRooms);


            return roomTypes.Select(rt => new RoomTypeDto(
                rt.Id,
                rt.Name,
                rt.Capacity,
                rt.PricePerNight,
                includeRooms && rt.Rooms != null
                    ? rt.Rooms.Select(r => new RoomDto(
                        r.Id,
                        r.Number
                      )).ToList()
                    : null
                    )).ToList();
        }
    }
}
 