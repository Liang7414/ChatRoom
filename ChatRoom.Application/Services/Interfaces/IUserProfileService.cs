using ChatRoom.Application.DTOs;

namespace ChatRoom.Application.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileResponseDTO?> GetProfileByUserIdAsync(int userId);
        Task UpdateProfileAsync(int userId, UpdateUserProfileRequestDTO updateDto,string IpAddress);
    }
}