namespace ChatRoom.Application.DTOs
{
    public class UpdateUserProfileRequestDTO
    {
        // 回傳給前端的資料
        public string? NickName { get; set; }
        public string? PortraitPhoto { get; set; }
    }
}
