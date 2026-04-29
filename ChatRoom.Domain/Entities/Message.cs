using ChatRoom.Domain.Entities.UserAggregate;

namespace ChatRoom.Domain.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual UserProfile UserProfile { get; set; } = null!;
    }
}
