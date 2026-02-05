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
        [HttpPost]
        public async Task<IActionResult> Create([FromServices] CreateRoomHandler createRoomHandler, [FromBody] CreateRoomCommand command)
        {
            await createRoomHandler.Handle(command);

            return Ok("Room created successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetRoomsHandler getRoomsHandler)
        {
            var rooms = await getRoomsHandler.Handle();

            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromServices] GetRoomByIdHandler getRoomByIdHandler, int id)
        {
            var room = await getRoomByIdHandler.Handle(id);
            if (room == null)
                return NotFound();

            return Ok(room);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromServices] DeleteRoomHandler deleteRoomHandler, int id)
        {
            await deleteRoomHandler.Handle(id);
            return Ok("Room deleted successfully");
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromServices] EditRoomHandler editRoomHandler, [FromBody] RoomDto room)
        {
            await editRoomHandler.Handle(room);

            return Ok("Room updated successfully");
        }
    }
}
