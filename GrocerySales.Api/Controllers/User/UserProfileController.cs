using GrocerySales.Abstractions.Dtos.User;
using GrocerySales.Abstractions.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GrocerySales.Api.Controllers.User
{
    [Route("api/user-profile")]
    [ApiController]
    public class UserProfileController(IUserProfileService userProfileService) : ControllerBase
    {
        [HttpGet("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserProfileAsync(Guid userId)
        {
            var userProfile = await userProfileService.GetUserProfileByIdAsync(userId);
            if (userProfile == null)
            {
                return NotFound("User profile not found");
            }
            return Ok(userProfile);
        }

        [HttpPut("{userId}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserProfileAsync(Guid userId, [FromBody] UserProfileRespone userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userProfile == null || userId != userProfile.UserId)
            {
                return BadRequest("Invalid user profile data");
            }
            var updatedProfile = await userProfileService.UpdateUserProfileAsync(userId, userProfile);
            if (updatedProfile != null)
            {
                return Ok("Update successful");
            }
            return BadRequest("Update not successful");
        }

        [HttpPost("{userId}/avatar")]
        [Authorize]
        public async Task<IActionResult> UpdateAvatarAsync(Guid userId, [FromBody] string avatarUrl)
        {
            if (string.IsNullOrEmpty(avatarUrl))
            {
                return BadRequest("Avatar URL cannot be empty");
            }
            var updatedAvatar = await userProfileService.UpdateAvatarAsync(userId, avatarUrl);
            if (updatedAvatar != null)
            {
                return Ok(updatedAvatar);
            }
            return BadRequest("Update avatar not successful");
        }
    }
}
