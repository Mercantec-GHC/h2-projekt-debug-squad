using Application.Guests.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // IConfiguration provides access to appsettings.json and other configuration sources
        private readonly IConfiguration _configuration;

        // Constructor receives IConfiguration via dependency injection
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // POST: api/auth/login
        // This method handles login requests
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromServices] GetAllGuestsHandler handler, [FromBody] LoginCommand command)
        {
            try
            {
                // Validate input: email must not be empty
                if (string.IsNullOrWhiteSpace(command.Email))
                    return BadRequest("Email is required");

                // Retrieve all guests from the handler
                var guests = await handler.Handle();

                // Look for a guest with matching email (case-insensitive)
                var guest = guests.FirstOrDefault(g => g.Email.Equals(command.Email, StringComparison.OrdinalIgnoreCase));

                // If no guest found → return 401 Unauthorized
                if (guest == null)
                    return Unauthorized("Guest not found");

                // Generate JWT token for the authenticated guest
                var token = GenerateJwtToken(guest);

                // Return token and guest name in response DTO
                return Ok(new LoginResponseDto(token, guest.FullName));
            }
            catch (Exception)
            {
                // Return generic server error in case of unexpected exceptions
                return StatusCode(500, "Internal server error");
            }
        }

        // Helper method to generate JWT token for a guest
        private string GenerateJwtToken(GuestDto guest)
        {
            // Read secret key from configuration and convert to byte array
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            // Create signing credentials using HMAC SHA256 algorithm
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Define claims to include in the token
            var claims = new Claim[]
            {
                new(ClaimTypes.NameIdentifier, guest.Id.ToString()), // User ID
                new(ClaimTypes.Name, guest.FullName),               // Full name
                new(ClaimTypes.Email, guest.Email)                  // Email
            };

            // Create JWT token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],       // Who issued the token
                audience: _configuration["Jwt:Audience"],   // Intended audience
                claims: claims,                             // Claims to include
                expires: DateTime.UtcNow.AddHours(2),       // Expiration time
                signingCredentials: credentials            // Signature
            );

            // Convert the token object to a string (JWT format: header.payload.signature)
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}