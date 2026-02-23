using Application.RoomTypes.Handlers;
using Application.RoomTypes.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomTypesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetAllRoomTypesHandler handler)
        {
            var roomTypes = await handler.Handle();
            return Ok(roomTypes);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromServices] EditRoomTypeHandler handler, [FromBody] EditRoomTypeCommand command)
        {
            await handler.Handle(command);

            return Ok("Room type successfully edited");
        }
    }
}
