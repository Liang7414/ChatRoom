using ChatRoom.Domain.Entities.UserAggregate;
using ChatRoom.Domain.RepositoryInterfaces;
using ChatRoom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Infrastructure.RepositoryImplementations
{
    public class UserLogRepository : IUserLogRepository
    {
        private readonly ChatRoomDbContext _context;

        public UserLogRepository(ChatRoomDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserLog log)
        {
            await _context.UserLogs.AddAsync(log);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserLog>> GetByUserIdAsync(int userId)
        {
            return await _context.UserLogs
                .Include(log => log.EventType)
                .Where(log => log.UserId == userId)
                .OrderByDescending(log => log.CreatedAt)
                .Take(10)
                .ToListAsync();
        }
    }
}
