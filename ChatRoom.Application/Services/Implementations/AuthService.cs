using AutoMapper;
using BCrypt.Net;
using ChatRoom.Application.DTOs;
using ChatRoom.Application.Services.Interfaces;
using ChatRoom.Domain.Entities.UserAggregate;
using ChatRoom.Domain.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace ChatRoom.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserLogService _userLogService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;


    public AuthService(IConfiguration configuration, IUserRepository userRepository, IMapper mapper, IUserLogService userLogService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _configuration = configuration;
        _userLogService = userLogService;
    }

    public async Task<UserRegisterResponseDTO> RegisterAsync(UserRegisterDTO registerDto, string ipAddress)
    {
        // 檢查帳號是否重複
        if (await _userRepository.GetByUsernameAsync(registerDto.Name) != null)
            throw new Exception("該帳號已被註冊");

        // 使用 AutoMapper 將 DTO 轉為 Entity
        var user = _mapper.Map<User>(registerDto);

        // 密碼雜湊 (不存明碼)
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

        // 為新註冊的使用者創建一個預設的 UserProfile
        user.Profile = new UserProfile
        {
            NickName = user.Name,   // 新創帳號預設暱稱為帳號名稱
            PortraitPhoto = "default.png"
        };

        // 存入UserProfile資料庫
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        // 記錄註冊事件
        await _userLogService.CreateLogAsync(user.Id, eventId: 4, ipAddress, desc: "User registered and default profile created");

        return _mapper.Map<UserRegisterResponseDTO>(user);
    }

    public async Task<UserLoginResponseDTO> LoginAsync(UserLoginDTO loginDto, string ipAddress)
    {

        // 根據帳號查詢使用者
        var user = await _userRepository.GetByUsernameAsync(loginDto.Name);
        if (user == null)
            throw new Exception("帳號不存在");
        // 驗證密碼
        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            throw new Exception("密碼錯誤");

        // 使用 AutoMapper 將 Entity 轉為 DTO
        var response = _mapper.Map<UserLoginResponseDTO>(user);

        // 生成 JWT Token
        response.Token = GenerateJwtToken(user);

        // 記錄登入事件
        await _userLogService.CreateLogAsync(user.Id, eventId: 1, ipAddress, desc: null);

        return response;
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Name),
        new Claim("ProfileId", user.Profile.Id.ToString())
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public async Task LogoutAsync(int userId, string ipAddress)
    {
        // 如果使用的是無狀態的 JWT，通常不需要在伺服器端做任何事情
        // 但如果有實作黑名單或是 token 失效機制，可以在這裡將 token 加入黑名單

        // 記錄登出事件
        await _userLogService.CreateLogAsync(userId, eventId: 2, ipAddress, desc: null);
    }
}


