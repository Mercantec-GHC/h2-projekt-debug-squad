using Application.Guests.Handlers;
using Application.Guests.Queries;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromServices] AddGuestHandler handler, [FromBody] AddGuestCommand command)
        {
            try
            {
                await handler.Handle(command);
                return Ok("Guest created successfully");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetAllGuestsHandler handler)
        {
            try
            {
                var guests = await handler.Handle();
                return Ok(guests);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromServices] GetGuestByIdHandler handler, int id)
        {
            try
            {
                var guest = await handler.Handle(new GuestByIdQuery(id));

                if (guest == null)
                    return NotFound("Guest not found");

                return Ok(guest);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromServices] DeleteGuestHandler handler, int id)
        {
            try
            {
                await handler.Handle(id);
                return Ok("Guest deleted successfully");
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
        public async Task<IActionResult> Edit([FromServices] EditGuestHandler handler, [FromBody] EditGuestCommand command)
        {
            try
            {
                await handler.Handle(command);
                return Ok("Guest updated successfully");
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromServices] RegisterGuestHandler handler, [FromBody] RegisterGuestCommand command)
        {
            try
            {
                await handler.Handle(command);
                return Ok("Guest registered successfully");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("check")]
        public async Task<IActionResult> CheckGuest([FromServices] GetAllGuestsHandler handler, [FromQuery] string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return BadRequest("Email is required");

                var guests = await handler.Handle();
                var exists = guests.Any(g =>
                    g.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                return Ok(exists);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("byemail")]
        public async Task<IActionResult> GetByEmail([FromServices] GetAllGuestsHandler handler, [FromQuery] string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return BadRequest("Email is required");

                var guests = await handler.Handle();
                var guest = guests.FirstOrDefault(g =>
                    g.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                if (guest == null)
                    return NotFound("Guest not found");

                return Ok(guest);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}