using ChatRoom.Application.DTOs;
using ChatRoom.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatRoom.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserLogController : ControllerBase
    {
        private readonly IUserLogService _userLogService;

        public UserLogController(IUserLogService userLogService)
        {
            _userLogService = userLogService;
        }

        [HttpGet("Logs")]
        public async Task<ActionResult<IEnumerable<UserLogResponseDTO>>> GetMyLogs()
        {
            // 從 Token 提取 UserId
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var logs = await _userLogService.GetLogsByUserIdAsync(userId);
            return Ok(logs);
        }
    }
}
