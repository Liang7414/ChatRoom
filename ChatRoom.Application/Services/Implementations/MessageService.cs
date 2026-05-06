using AutoMapper;
using ChatRoom.Application.DTOs;
using ChatRoom.Application.Services.Interfaces;
using ChatRoom.Domain.Entities;
using ChatRoom.Domain.RepositoryInterfaces;

namespace ChatRoom.Application.Services.Implementations
{
    public class MessageService: IMessageService
    {
        private readonly IMapper _mapper;
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMapper mapper, IMessageRepository messageRepository)
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
        }

        // 取得訊息列表：回傳 DTO 包含 UserProfile 資訊
        public async Task<IEnumerable<MessageResponseDTO>> GetMessagesAsync(int userProfileId, int limit = 50)
        {
            var messages = await _messageRepository.GetRecentMessagesAsync(limit);

            return messages.Select(m =>
            {
                var dto = _mapper.Map<MessageResponseDTO>(m);

                // 1. 判定是否為當前使用者發送 (用於前端 UI 排版)
                dto.IsOwnMessage = m.UserProfileId == userProfileId;

                // 2. 處理標記刪除的內容顯示
                if (m.IsDeleted)
                {
                    dto.Content = "此訊息已被發言者移除";
                }
                // 3. 處理 AI 違規偵測的內容顯示 (若尚未刪除但違規)
                else if (m.IsViolation)
                {
                    dto.Content = "此訊息因違反社群規範已被屏蔽";
                }

                return dto;
            });
        }

        // 發送訊息：新增一筆 Message 資料，並回傳包含 UserProfile 資訊的 DTO
        public async Task<MessageResponseDTO> SendMessageAsync(int userProfileId, string content)
        {
            var message = new Message
            {
                UserProfileId = userProfileId,
                Content = content,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
                IsViolation = false // 預設為正常，後續可串接 AI 檢測 Service
            };

            await _messageRepository.AddAsync(message);
            await _messageRepository.SaveChangesAsync();

            // 重新抓取包含 UserProfile 的完整資料以回傳 DTO
            var savedMessage = await _messageRepository.GetByIdAsync(message.Id);
            var result = _mapper.Map<MessageResponseDTO>(savedMessage);
            result.IsOwnMessage = true;

            return result;
        }

        // 軟刪除訊息：將 IsDeleted 標記為 true，但不從資料庫中物理刪除
        public async Task<bool> SoftDeleteMessageAsync(int messageId, int UserProfileId)
        {
            var message = await _messageRepository.GetByIdAsync(messageId);

            if (message == null) return false;

            // 權限檢查：僅本人可刪除 (未來可加入管理員邏輯)
            if (message.UserProfileId != UserProfileId)
            {
                throw new UnauthorizedAccessException("您沒有權限刪除此訊息。");
            }

            message.IsDeleted = true;

            await _messageRepository.SaveChangesAsync();
            return true;
        }
    }
}
