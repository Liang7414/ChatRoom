using AutoMapper;
using ChatRoom.Application.DTOs;
using ChatRoom.Domain.Entities;
using ChatRoom.Domain.Entities.UserAggregate;

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

            CreateMap<Message, MessageResponseDTO>()
                .ForMember(dest => dest.MessageId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nickname, opt => opt.MapFrom(src => src.UserProfile.NickName))
                .ForMember(dest => dest.PortraitPhoto, opt => opt.MapFrom(src => src.UserProfile.PortraitPhoto))
                // IsOwnMessage 的值由 Service 層邏輯決定，Mapping 時忽略
                .ForMember(dest => dest.IsOwnMessage, opt => opt.Ignore());
        }
    }
}