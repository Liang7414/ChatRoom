using ChatRoom.Domain.Entities;
using ChatRoom.Domain.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Infrastructure.Data
{
    public class ChatRoomDbContext : DbContext
    {
        public ChatRoomDbContext(DbContextOptions<ChatRoomDbContext> options) : base(options)
        {
        }

        // 定義資料表 (DbSet)
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }
        public DbSet<UserEventType> UserEventTypes { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 配置 User 實體
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PasswordHash)
                      .IsRequired()
                      .IsUnicode(false)
                      .HasMaxLength(255);
            });

            // 配置 User 與 UserProfile 的「一對一」關係
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(p => p.UserId);

            // 配置 UserProfile 實體
            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.Property(e => e.NickName).IsRequired().HasMaxLength(30);
                entity.Property(e => e.PortraitPhoto).HasMaxLength(255);
            });

            // UserProfile 與 Message 的 1對多 關係
            modelBuilder.Entity<Message>()
                .HasOne(m => m.UserProfile)
                .WithMany(p => p.Messages)
                .HasForeignKey(m => m.UserProfileId)
                .OnDelete(DeleteBehavior.Cascade); // 如果 Profile 刪除，訊息也一併刪除

            // 配置 UserLog 與 UserEventType 的「多對一」關係
            modelBuilder.Entity<UserLog>()
                .HasOne(l => l.EventType)
                .WithMany(t => t.Logs)
                .HasForeignKey(l => l.EventId);

        }
    }
}
