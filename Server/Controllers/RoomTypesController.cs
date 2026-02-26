using Application.RoomTypes.Handlers;
using Shared;
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
            try
            {
                var roomTypes = await handler.Handle();
                return Ok(roomTypes);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("available")]
        public async Task<IActionResult> GetAvailable([FromServices] GetAvailableRoomTypesHandler handler, [FromBody] GetAvailableRoomTypesCommand command)
        {
            try
            {
                var roomTypes = await handler.Handle(command);
                return Ok(roomTypes);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromServices] EditRoomTypeHandler handler, [FromBody] EditRoomTypeCommand command)
        {
            try
            {
                await handler.Handle(command);
                return Ok("Room type successfully edited");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}