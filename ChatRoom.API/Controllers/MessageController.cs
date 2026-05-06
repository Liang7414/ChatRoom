using ChatRoom.Application.DTOs;
using ChatRoom.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatRoom.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        private int UserProfileId=>int.Parse(User.Claims.FirstOrDefault(c => c.Type == "ProfileId")?.Value
                    ?? throw new UnauthorizedAccessException("無法從 Token 識別個人檔案身分"));

        [HttpGet("history")]
        public async Task<ActionResult<IEnumerable<MessageResponseDTO>>> GetHistory([FromQuery] int limit = 50)
        {
            // 傳入 userProfileId 以便 Service 判斷 IsOwnMessage
            var history = await _messageService.GetMessagesAsync(UserProfileId, limit);
            return Ok(history);
        }

        [HttpPost]
        public async Task<ActionResult<MessageResponseDTO>> SendMessage([FromBody] string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return BadRequest("訊息內容不可為空");
            }

            // 直接調用 Service，參數名稱純粹為 userProfileId
            var result = await _messageService.SendMessageAsync(UserProfileId, content);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            try
            {
                var success = await _messageService.SoftDeleteMessageAsync(id, UserProfileId);

                if (!success)
                {
                    return NotFound("找不到該訊息，或訊息已不存在");
                }

                return NoContent(); // 204 No Content 代表成功處理但無須回傳內容
            }
            catch (UnauthorizedAccessException ex)
            {
                // 若 Service 判定該訊息不屬於此 userProfileId，則回傳 403
                return Forbid(ex.Message);
            }
        }

    }
}
