using Application.Rooms.Commands;
using Application.Rooms.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly CreateRoomHandler _createRoomHandler;

        public RoomsController(CreateRoomHandler createRoomHandler)
        {
            _createRoomHandler = createRoomHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoomCommand command)
        {
            await _createRoomHandler.Handle(command);

            return Ok("Room created successfully");
        }
    }
}
