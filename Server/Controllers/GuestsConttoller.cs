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
        public async Task<IActionResult> GetAll(
            [FromServices] GetGuestsHandler getGuestsHandler)
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

    }
}

