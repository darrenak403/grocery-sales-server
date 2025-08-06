using GrocerySales.Abstractions.Dtos.Account;
using GrocerySales.Abstractions.Dtos.User;
using GrocerySales.Abstractions.Entities;
using GrocerySales.Abstractions.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrocerySales.Api.Controllers.Customer
{
    [Route("api/users")]
    [ApiController]
    public class CustomerController(IUserService userService) : ControllerBase
    {

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await userService.GetAllUserAsync();
            return Ok(users);
        }
        [HttpPut("{userId}/ban")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> BanUserAsync(Guid userId)
        {
            var user = await userService.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            await userService.BanAsync(userId);
            return Ok("User banned successfully");
        }

        [HttpPut("{userId}/unban")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> UnbanUserAsync(Guid userId)
        {
            var user = await userService.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            await userService.UnbanAsync(userId);
            return Ok("User unbanned successfully");
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> AddUser([FromBody] AccountRequest request)
        {
            var newUser = await userService.AddUserAsync(request);
            return CreatedAtAction(nameof(GetAllUsersAsync), new { id = newUser?.UserId }, newUser);
        }
    }
}
