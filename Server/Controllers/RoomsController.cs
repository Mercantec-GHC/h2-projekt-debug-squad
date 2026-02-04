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
        private readonly GetRoomByIdHandler _getRoomByIdHandler;
        private readonly DeleteRoomHandler _deleteRoomHandler;

        public RoomsController(
            CreateRoomHandler createRoomHandler,
            GetRoomsHandler getRoomsHandler,
            GetRoomByIdHandler getRoomByIdHandler,
            DeleteRoomHandler deleteRoomHandler
            )
        {
            _createRoomHandler = createRoomHandler;
            _getRoomsHandler = getRoomsHandler;
            _getRoomByIdHandler = getRoomByIdHandler;
            _deleteRoomHandler = deleteRoomHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoomCommand command)
        {
            await _createRoomHandler.Handle(command);

            return Ok("Room created successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rooms = await _getRoomsHandler.Handle();

            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var room = await _getRoomByIdHandler.Handle(id);
            if (room == null)
                return NotFound();

            return Ok(room);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _deleteRoomHandler.Handle(id);
            return Ok("Room deleted successfully");
        }
    }
}
