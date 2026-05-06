using ChatRoom.Domain.Entities.UserAggregate;
using ChatRoom.Domain.RepositoryInterfaces;
using ChatRoom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Infrastructure.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ChatRoomDbContext _context;

        public UserProfileRepository(ChatRoomDbContext context)
        {
            _context = context;
        }

        // 根據 UserId 抓取 Profile
        public async Task<UserProfile?> GetByUserIdAsync(int userId)
        {
            return await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        // 用於註冊流程中新增預設 Profile
        public async Task AddAsync(UserProfile profile)
        {
            await _context.UserProfiles.AddAsync(profile);
        }

        // 如果未來使用者想修改暱稱或換頭像會用到
        public async Task UpdateAsync(UserProfile profile)
        {
            _context.UserProfiles.Update(profile);
            await Task.CompletedTask; // Update 在 EF Core 中通常不是非同步的
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
