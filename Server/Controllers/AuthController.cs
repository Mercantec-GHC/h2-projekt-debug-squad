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
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromServices] GetAllGuestsHandler handler, [FromBody] LoginCommand command)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(command.Email))
                    return BadRequest("Email is required");

                var guests = await handler.Handle();
                var guest = guests.FirstOrDefault(g =>
                    g.Email.Equals(command.Email, StringComparison.OrdinalIgnoreCase));

                if (guest == null)
                    return Unauthorized("Guest not found");

                var token = GenerateJwtToken(guest);
                return Ok(new LoginResponseDto(token, guest.FullName));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        private string GenerateJwtToken(GuestDto guest)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, guest.Id.ToString()),
                new Claim(ClaimTypes.Name, guest.FullName),
                new Claim(ClaimTypes.Email, guest.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
