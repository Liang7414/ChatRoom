namespace ChatRoom.Domain.Entities.UserAggregate
{
    public class UserProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? PortraitPhoto { get; set; }
        public string? NickName { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
