using ChatRoom.Application.DTOs;
using ChatRoom.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatRoom.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IUserProfileService _profileService;

        public ProfileController(IUserProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("me")]
        public async Task<ActionResult<UserProfileResponseDTO>> GetMyProfile()
        {
            // 從 JWT Claim 提取真實 UserId，確保 User 只能讀自己的
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _profileService.GetProfileByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileRequestDTO request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            await _profileService.UpdateProfileAsync(userId, request, IpAddress: ip);

            return NoContent();
        }
    }
}
