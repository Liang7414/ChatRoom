using AutoMapper;
using ChatRoom.Application.DTOs;
using ChatRoom.Application.Services.Interfaces;
using ChatRoom.Domain.RepositoryInterfaces;


namespace ChatRoom.Application.Services.Implementations
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _profileRepository;
        private readonly IMapper _mapper;
        private readonly IUserLogService _userLogService;

        public UserProfileService(IUserProfileRepository profileRepository, IMapper mapper, IUserLogService userLogService)
        {
            _profileRepository = profileRepository;
            _userLogService = userLogService;
            _mapper = mapper;
        }

        // 回傳 ResponseDTO
        public async Task<UserProfileResponseDTO?> GetProfileByUserIdAsync(int userId)
        {
            // 引用Repository取得實體，使用AutoMapper轉成ResponseDTO
            var profile = await _profileRepository.GetByUserIdAsync(userId);
            return _mapper.Map<UserProfileResponseDTO>(profile);
        }

        // 接收 RequestDTO

        public async Task UpdateProfileAsync(int userId, UpdateUserProfileRequestDTO request, string IpAddress)
        {
            var profile = await _profileRepository.GetByUserIdAsync(userId);
            if (profile == null) throw new Exception("找不到個人檔案");

            // 收集變更內容以便記錄，必須在SaveChangesAsync前收集原始值，才能正確描述變更
            List<string> changes = new();

            if (request.NickName != null)
                changes.Add($"暱稱由 {profile.NickName} 改為 {request.NickName}");

            if (request.PortraitPhoto != null)
                changes.Add("更新了頭像");

            // 使用 AutoMapper 將 Request 的值蓋到實體上
            _mapper.Map(request, profile);

            await _profileRepository.UpdateAsync(profile);
            await _profileRepository.SaveChangesAsync();

            
            string finalDesc = changes.Count > 0
                ? $"編輯個人資料: {string.Join(", ", changes)}"
                : "編輯個人資料 (無實質變更)";

            await _userLogService.CreateLogAsync(userId, eventId:3, IpAddress, desc: finalDesc);

        }
    }
}
