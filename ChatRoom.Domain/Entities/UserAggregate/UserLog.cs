
namespace ChatRoom.Domain.Entities.UserAggregate
{
    public class UserLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public string IpAddress { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual User User { get; set; } = null!;
        public virtual UserEventType EventType { get; set; } = null!;
    }
}
