using Application.DayMultipliers.Handlers;
using Microsoft.AspNetCore.Mvc;

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
    }
}
