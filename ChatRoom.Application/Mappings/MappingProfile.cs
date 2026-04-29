using ChatRoom.Domain.Entities.UserAggregate;
using AutoMapper;
using ChatRoom.Application.DTOs;

namespace ChatRoom.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // --- 註冊相關 ---
            CreateMap<UserRegisterDTO, User>();
            CreateMap<User, UserRegisterResponseDTO>();

            // --- 登入相關 ---
            CreateMap<User, UserLoginResponseDTO>()
                .ForMember(dest => dest.Token, opt => opt.Ignore())  // Token 由 Service 邏輯產生，Mapping 時忽略
                .ForMember(dest => dest.Nickname, opt => opt.MapFrom(src => src.Profile.NickName))
                .ForMember(dest => dest.PortraitPhoto, opt => opt.MapFrom(src => src.Profile.PortraitPhoto));

            // --- 個人檔案相關 ---
            CreateMap<UserProfile, UserProfileResponseDTO>();

            // 從 Request 直接更新到 Entity
            CreateMap<UpdateUserProfileRequestDTO, UserProfile>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // --- 行為日誌相關 ---
            CreateMap<UserLog, UserLogResponseDTO>()
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.EventType.EventType));
        }
    }
}