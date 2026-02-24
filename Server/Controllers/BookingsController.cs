using Application.Bookings.Handlers;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromServices] CreateBookingHandler handler, [FromBody] CreateBookingCommand command)
        {
            try
            {
                await handler.Handle(command);
                return Ok("Booking created successfully");
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
        public async Task<IActionResult> GetAll([FromServices] GetAllBookingsHandler handler)
        {
            try
            {
                var bookings = await handler.Handle();
                return Ok(bookings);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromServices] GetBookingByIdHandler handler,
            int id)
        {
            try
            {
                var booking = await handler.Handle(id);

                if (booking == null)
                    return NotFound("Booking not found");

                return Ok(booking);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("guest/{id}")]
        public async Task<IActionResult> GetByGuestId([FromServices] GetBookingsByGuestIdHandler handler, int id)
        {
            try
            {
                var bookings = await handler.Handle(id);
                return Ok(bookings);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromServices] EditBookingHandler handler, [FromBody] EditBookingCommand command)
        {
            try
            {
                await handler.Handle(command);
                return Ok("Booking updated successfully");
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

        [HttpDelete]
        public async Task<IActionResult> Delete([FromServices] DeleteBookingHandler handler, [FromBody] DeleteBookingCommand command)
        {
            try
            {
                await handler.Handle(command);
                return Ok("Booking deleted successfully");
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

        [HttpGet("summary")]
        public async Task<IActionResult> GetAllSummary([FromServices] GetBookingSummariesHandler handler)
        {
            try
            {
                var bookings = await handler.Handle();
                return Ok(bookings);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
