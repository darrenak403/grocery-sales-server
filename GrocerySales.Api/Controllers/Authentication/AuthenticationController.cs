using GrocerySales.Abstractions.Dtos.Authentication;
using GrocerySales.Abstractions.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GrocerySales.Api.Controllers.Authentication
{

    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController(IAuthService authService) : ControllerBase
    {
        [HttpPost("check-login")]
        public async Task<IActionResult> CheckLogin([FromBody] UserLoginRequest request)
        {
            if (request is null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid login request.");
            }
            var isExist = await authService.CheckLoginAsync(request);
            if (!isExist)
            {
                return NotFound("Invalid email or password.");
            }
            return Ok(isExist);
        }

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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            var result = await authService.RegisterAsync(request);

            if (!result)
            {
                return Conflict(new { message = "Email or phone number already exists." });
            }

            return Ok(true);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (request is null || request.UserId == Guid.Empty || string.IsNullOrEmpty(request.RefreshToken))
            {
                return BadRequest("Invalid refresh token request.");
            }
            var tokenResponse = await authService.RefreshTokenAsync(request);
            if (tokenResponse == null)
            {
                return BadRequest("Invalid refresh token.");
            }
            return Ok(tokenResponse);
        }

    }
}
