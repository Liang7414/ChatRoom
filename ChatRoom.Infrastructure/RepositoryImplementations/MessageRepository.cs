using ChatRoom.Domain.Entities;
using ChatRoom.Domain.RepositoryInterfaces;
using ChatRoom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Infrastructure.RepositoryImplementations
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatRoomDbContext _context;

        public MessageRepository(ChatRoomDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetRecentMessagesAsync(int limit)
        {
            return await _context.Messages
                .AsNoTracking()
                .Include(m => m.UserProfile)
                .OrderByDescending(m => m.CreatedAt)
                .Take(limit)
                .Reverse() // 轉回正確的時間線順序
                .ToListAsync();
        }
        public async Task<Message?> GetByIdAsync(int id)
        {
            return await _context.Messages
                .Include(m => m.UserProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}