using Application.RoomTypes.Handlers;
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
    }
}
