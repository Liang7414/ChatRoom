using ChatRoom.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChatRoom.Domain.Entities.UserAggregate.UserEventType;

namespace ChatRoom.Application.Services.Interfaces
{
    public interface IUserLogService
    {
        Task CreateLogAsync(int userId, int eventId, string ip, string? desc = null);

        Task<IEnumerable<UserLogResponseDTO>> GetLogsByUserIdAsync(int userId);
    }
}
