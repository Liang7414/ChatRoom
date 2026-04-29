using ChatRoom.Application.DTOs;

namespace ChatRoom.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserRegisterResponseDTO> RegisterAsync(UserRegisterDTO registerDto, string ipAddress);
        Task<UserLoginResponseDTO> LoginAsync(UserLoginDTO loginDto, string ipAddress);
        Task  LogoutAsync(int userId, string ipAddress);
    }
}
