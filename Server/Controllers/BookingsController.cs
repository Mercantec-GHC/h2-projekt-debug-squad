using Application.Bookings.Commands;
using Application.Bookings.Handlers;
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

            return Ok("Room created successfully");
        }
    }
}
