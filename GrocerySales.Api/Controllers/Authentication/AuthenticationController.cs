using GrocerySales.Abstractions.Dtos.Authentication;
using GrocerySales.Abstractions.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GrocerySales.Api.Controllers.Authentication
{

    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            if (request is null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid login request.");
            }
            var tokenResponse = await authService.LoginAsync(request);
            if (tokenResponse == null)
            {
                return BadRequest("Invalid email or password.");
            }
            return Ok(tokenResponse);
        }
    }
}
