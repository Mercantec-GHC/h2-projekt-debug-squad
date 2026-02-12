using Application.Guests.Commands;
using Application.Guests.Handlers;
using Application.Guests.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromServices] AddGuestHandler addGuestHandler,
            [FromBody] AddGuestCommand command)
        {
            await addGuestHandler.Handle(command);
            return Ok("Guest created successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetGuestsHandler getGuestsHandler)
        {
            var guests = await getGuestsHandler.Handle();
            return Ok(guests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromServices] GetGuestByIdHandler getGuestByIdHandler, int id)
        {
            var guest = await getGuestByIdHandler.Handle(new GuestByIdQuery(id));

            if (guest == null)
                return NotFound();

            return Ok(guest);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromServices] DeleteGuestHandler deleteGuestHandler, int id)
        {
            await deleteGuestHandler.Handle(id);
            return Ok("Guest deleted successfully");
        }


        [HttpPut("edit")]
        public async Task<IActionResult> Edit( [FromServices] EditGuestHandler editGuestHandler,[FromBody] EditGuestCommand command)
        {
            try
            {
                await editGuestHandler.Handle(command);
                return Ok("Guest updated successfully");
            }
            catch (Exception ex)
            {
                // Return detailed info for debugging
                return BadRequest(new { ex.Message, ex.StackTrace });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
           [FromServices] RegisterGuestHandler handler,
           [FromBody] RegisterGuestCommand command)
        {
            try
            {
                await handler.Handle(command);
                return Ok("Guest registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet("check")]
        public async Task<IActionResult> CheckGuest([FromServices] GetGuestsHandler getGuestsHandler, [FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is required");

            var guests = await getGuestsHandler.Handle();
            var exists = guests.Any(g => g.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            return Ok(exists); // returns true or false
        }


        [HttpGet("byemail")]
        public async Task<IActionResult> GetByEmail([FromServices] GetGuestsHandler getGuestsHandler, [FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is required");

            var guests = await getGuestsHandler.Handle();
            var guest = guests.FirstOrDefault(g => g.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (guest == null)
                return NotFound("Guest not found");

            return Ok(guest); // returns full guest object including Id
        }

    }

}

