using Application.Rooms.Commands;
using Application.Rooms.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    // Marks this class as an API controller.
    // Enables features like automatic model binding and validation.
    [ApiController]
    // Defines the base route for this controller: "api/rooms"
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        // Application-layer handler responsible for the "Create Room" use case
        private readonly CreateRoomHandler _createRoomHandler;

        // The handler is injected via Dependency Injection 
        public RoomsController(CreateRoomHandler createRoomHandler)
        {
            _createRoomHandler = createRoomHandler;
        }

        // Handles HTTP POST requests to "api/rooms"
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoomCommand command)
        {
            // Delegate the business logic to the application layer
            // The controller itself contains no business rules
            await _createRoomHandler.Handle(command);

            // Return an HTTP 200 OK response to the client
            return Ok("Room created successfully");
        }
    }
}
