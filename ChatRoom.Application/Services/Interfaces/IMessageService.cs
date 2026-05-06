using ChatRoom.Application.DTOs;
using System.Threading.Tasks;

namespace ChatRoom.Application.Services.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageResponseDTO>> GetMessagesAsync(int userProfileId, int limit);
        Task<MessageResponseDTO> SendMessageAsync(int userProfileId, string content);
        Task<bool> SoftDeleteMessageAsync(int messageId, int UserProfileId);
    }
}
