using Application.Bookings.Commands;
using Application.Bookings.Handlers;
using Application.Guests.Handlers;
using Application.Guests.Queries;
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

        [HttpGet()]
        public async Task<IActionResult> GetAll([FromServices] GetAllBookingsHandler getAllBookingsHandler)
        {
            var bookings = await getAllBookingsHandler.Handle();

            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromServices] GetBookingByIdHandler getBookingByIdHandler, int id)
        {
            var booking = await getBookingByIdHandler.Handle(id);

            if (booking == null)
                return NotFound("The booking ID is invalid");

            return Ok(booking);
        }

        [HttpGet("guest/{id}")]
        public async Task<IActionResult> GetByGuestId([FromServices] GetBookingsByGuestIdHandler getBookingsByGuestIdHandler, int id)
        {
            var bookings = await getBookingsByGuestIdHandler.Handle(id);

            if (bookings == null)
                return NotFound("The ID of the Guest is invalid");

            return Ok(bookings);
        }

        [HttpDelete()]
        public async Task<IActionResult> Delete([FromServices] DeleteBookingHandler deleteBookingHandler, [FromBody] DeleteBookingCommand command)
        {
            if (await deleteBookingHandler.Handle(command)) return Ok("Booking deleted successfully");

            return BadRequest("One of the IDs was invalid");
        }
    }
}
