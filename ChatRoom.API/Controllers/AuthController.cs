using ChatRoom.Application.DTOs;
using ChatRoom.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatRoom.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO registerDto)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "Unknown";
                var response = await _authService.RegisterAsync(registerDto, ipAddress);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO loginDto)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "Unknown";
                var response = await _authService.LoginAsync(loginDto, ipAddress);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // 從 Token 中提取 UserId
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "Unknown";

            await _authService.LogoutAsync(userId, ipAddress);

            return Ok(new { message = "登出成功" });
        }
    }
}
