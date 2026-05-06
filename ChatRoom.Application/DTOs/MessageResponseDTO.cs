namespace ChatRoom.Application.DTOs
{
    public class MessageResponseDTO
    {
        public int MessageId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // 用戶資訊：顯示頭像與暱稱
        public string Nickname { get; set; } = string.Empty;
        public string PortraitPhoto { get; set; } = string.Empty;

        // 權限控制：讓前端知道這是不是「我」發的訊息
        public bool IsOwnMessage { get; set; }

        // 違規狀態：如果違規，前端可以選擇遮蔽文字
        public bool IsViolation { get; set; }
    }
}
