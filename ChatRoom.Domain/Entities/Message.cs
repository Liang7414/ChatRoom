using ChatRoom.Domain.Entities.UserAggregate;

namespace ChatRoom.Domain.Entities
{
    public class Message
    {
        public int Id { get; set; }
        // 外鍵：關聯到使用者個人檔案
        public int UserProfileId { get; set; }
        public string Content { get; set; } = string.Empty;
        // 訊息建立時間
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // --- 導覽屬性 ---
        public virtual UserProfile UserProfile { get; set; } = null!;

        // --- 訊息刪除 ---
        public bool IsDeleted { get; set; } = false;
        // 訊息刪除時間（如果已刪除）
        public DateTime? DeletedAt { get; set; }

        // --- AI 違規偵測預留欄位 ---
        /// 標記此訊息是否違反聊天室規範（由 AI 判定）
        public bool IsViolation { get; set; } = false;
        /// 違規的具體原因（例如：言語暴力、廣告、色情內容）
        public string? ViolationReason { get; set; }
    }
}
