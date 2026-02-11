using Application.Bookings.Commands;
using Application.Bookings.Handlers;
using Application.Rooms.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromServices] CreateBookingHandler createBookingHandler, [FromBody] CreateBookingCommand command)
        {
            await createBookingHandler.Handle(command);

            return Ok("Booking created successfully");
        }

        [HttpDelete()]
        public async Task<IActionResult> Delete([FromServices] DeleteBookingHandler deleteBookingHandler, [FromBody] DeleteBookingCommand command)
        {
            if (await deleteBookingHandler.Handle(command)) return Ok("Booking deleted successfully");

            return BadRequest("One of the IDs was invalid");
        }
    }
}
