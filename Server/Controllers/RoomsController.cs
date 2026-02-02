using Application.Rooms.Commands;
using Application.Rooms.Handlers;
using Application.Rooms.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly CreateRoomHandler _createRoomHandler;
        private readonly GetRoomsHandler _getRoomsHandler;

        public RoomsController(CreateRoomHandler createRoomHandler, GetRoomsHandler getRoomsHandler)
        {
            _createRoomHandler = createRoomHandler;
            _getRoomsHandler = getRoomsHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoomCommand command)
        {
            await _createRoomHandler.Handle(command);

            return Ok("Room created successfully");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rooms = await _getRoomsHandler.Handle(new GetAllRoomsQuery());
            return Ok(rooms);
        }
    }
}
