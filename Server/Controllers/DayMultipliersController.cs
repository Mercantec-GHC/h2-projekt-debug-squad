using Application.DayMultipliers.Handlers;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DayMultipliersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetAllDayMultipliersHandler handler)
        {
            try
            {
                var dayMultipliers = await handler.Handle();
                return Ok(dayMultipliers);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromServices] EditDayMultiplierHandler handler, [FromBody] EditDayMultiplierCommand command)
        {
            try
            {
                await handler.Handle(command);
                return Ok("Day multiplier updated successfully");
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