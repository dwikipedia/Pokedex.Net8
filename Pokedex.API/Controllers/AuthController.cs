using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Pokedex.Domain.Models;

namespace Pokedex.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/Auth")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] Auth request, [FromQuery] string? returnUrl = null)
        {
            if (request == null)
            {
                return BadRequest("Request body is null or invalid.");
            }

            if (request.Username != "dotnetwiki" || request.Password != "ch4rm@nder")
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, "Trainer")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("charmander-uses-waterfall-which-is-weird"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "PokedexAPI",
                audience: "PokedexClient",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                returnUrl = returnUrl ?? "/Pokemon"
            });
        }
    }
}
