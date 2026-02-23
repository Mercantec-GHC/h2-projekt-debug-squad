using Application.Rooms.Handlers;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromServices] CreateRoomHandler handler, [FromBody] CreateRoomCommand command)
        {
            try
            {
                await handler.Handle(command);
                return Ok("Room created successfully");
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

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromServices] GetAllRoomsHandler handler)
        {
            try
            {
                var rooms = await handler.Handle();
                return Ok(rooms);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromServices] GetRoomByIdHandler handler, int id)
        {
            try
            {
                var room = await handler.Handle(id);

                if (room == null)
                    return NotFound("Room not found");

                return Ok(room);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("available")]
        public async Task<IActionResult> GetAvailable([FromServices] GetAvailableRoomsHandler handler, [FromBody] GetAvailableRoomsCommand command)
        {
            try
            {
                var rooms = await handler.Handle(command);
                return Ok(rooms);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromServices] DeleteRoomHandler handler, int id)
        {
            try
            {
                await handler.Handle(id);
                return Ok("Room deleted successfully");
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

        [HttpPut]
        public async Task<IActionResult> Edit([FromServices] EditRoomHandler handler, [FromBody] EditRoomCommand command)
        {
            try
            {
                await handler.Handle(command);
                return Ok("Room updated successfully");
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