using Application.DayMultipliers.Commands;
using Application.DayMultipliers.Handlers;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DayMultipliersController : Controller
    {
        [HttpGet()]
        public async Task<IActionResult> GetAll([FromServices] GetAllDayMultipliersHandler getAllDayMultipliersHandler)
        {
            var dayMultipliers = await getAllDayMultipliersHandler.Handle();

            return Ok(dayMultipliers);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromServices] EditDayMultiplierHandler editDayMultiplierHandler, [FromBody] EditDayMultiplierCommand command)
        {
            bool response = await editDayMultiplierHandler.Handle(command);
            if (response) return Ok("Day multiplier updated successfully");

            return BadRequest("The specified id is invalid");
        }
    }
}
