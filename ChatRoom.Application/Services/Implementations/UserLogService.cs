using ChatRoom.Application.Services.Interfaces;
using ChatRoom.Domain.Entities.UserAggregate;
using ChatRoom.Domain.RepositoryInterfaces;
using ChatRoom.Application.DTOs;
using AutoMapper;
using Microsoft.Extensions.Logging;
using static ChatRoom.Domain.Entities.UserAggregate.UserEventType;

namespace ChatRoom.Application.Services.Implementations
{
    public class UserLogService : IUserLogService
    {
        private readonly IUserLogRepository _userLogRepository;
        private readonly IMapper _mapper;

        public UserLogService(IUserLogRepository userLogRepository, IMapper mapper)
        {
            _userLogRepository = userLogRepository;
            _mapper = mapper;
        }

        // 這裡的 eventId 可以對應到 UserEventType 的 enum 值，例如：
        // UserEventType.Login = 1, UserEventType.Logout = 2, UserEventType.Register = 3, UserEventType.ProfileUpdate = 4
        public async Task CreateLogAsync(int userId, int eventId, string ip, string? desc = null)
        {
            var log = new UserLog
            {
                UserId = userId,
                EventId = eventId,
                IpAddress = ip,
                Description = desc,
                CreatedAt = DateTime.UtcNow
            };
            await _userLogRepository.AddAsync(log);
            await _userLogRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserLogResponseDTO>> GetLogsByUserIdAsync(int userId)
        {
            var logs = await _userLogRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<UserLogResponseDTO>>(logs);


        }
    }
}
